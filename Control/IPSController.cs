//=========================================================
// This software is distributed under GPL v2 Licence
//
// Developed by Federico Castellari (fede.caste@gmail.com)
// November 2009
//
// Developed using Microsoft Visual C# 2008 Express Edition
//=========================================================

using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

using Castellari.IVaPS.Model;
using Castellari.IVaPS.View;
using Castellari.IVaPS.BLogic;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Castellari.IVaPS.Control
{
    /// <summary>
    /// Il main controller dell'applicazione, è da qui che si controllano View e Model come da pattern MVC,
    /// e da qui si attiva la business logic sincrona
    /// </summary>
    public class IPSController
    {
        /// <summary>
        /// Contenitore del Modello (inteso come nel pattern MVC) dell'applicazione. Qui si trovano i dati
        /// che vengono aggiornati dalla business logic e visualizzati dalla vista
        /// </summary>
        private FlightStatus status = null;
        /// <summary>
        /// Riferimento alla vista principale
        /// </summary>
        private MainForm viewMainForm = null;
        /// <summary>
        /// Riferimento al form di log. Probabilmente da rimuovere (NdF 20091114)
        /// </summary>
        private LogForm logForm = new LogForm();
        /// <summary>
        /// Utility per il logging dell'applicazione in opportuna form
        /// </summary>
        private LogUtil log = new LogUtil();
        /// <summary>
        /// Riferimento al wrapper di Flight Simulator (o meglio delle FSUIPC), è da qui che proverranno 
        /// gli eventi che porteranno a tenere aggiornato il modello
        /// </summary>
        private FSWrapper flightSim = null;
        /// <summary>
        /// il selection form dedicato alla selezione della sottosezione di checklist da leggere
        /// </summary>
        private TransparentChoiseForm checklistSelectionForm = null;
        /// <summary>
        /// Form di visualizzazione delle immagini
        /// </summary>
        private ImageViewer imageViewer = null;
        /// <summary>
        /// Barra delle utility
        /// </summary>
        private UtilityBar utilBar = null;
        /// <summary>
        /// Engine interno di TTS
        /// </summary>
        private ChecklistSpeaker checklistSpeaker = null;
        /// <summary>
        /// I/O pointer citato in issue 103, contiente l'ultima fase letta, negativo se mai letto nulla dal TTS
        /// </summary>
        private int lastPhaseNumber = -1;

        /// <summary>
        /// Costruttore. Richiede un riferimento alla vista principale per poter
        /// richiedere delle update della vista quando necessario
        /// </summary>
        /// <param name="view"></param>
        public IPSController(MainForm view)
        {
            Log("Init application...");
            viewMainForm = view;
            //Titolo della form principale con la versione dell'assembly
            //da modificare nelle proprietà di progetto SOLO da fede.caste in accordo con le milestones
            viewMainForm.Text = "IVaPS " + Assembly.GetExecutingAssembly().GetName().Version;

            #region Costruzione preliminare delle proprietà interne
            flightSim = new FSWrapper();
            status = new FlightStatus();
            checklistSpeaker = new ChecklistSpeaker();
            checklistSpeaker.Controller = this;
            utilBar = new UtilityBar();
            viewMainForm.mainPanel.SetStatus(status);
            

            try
            {
                IPSConfiguration.Load();
                Log("Configuration loaded");
                if (IPSConfiguration.AUTO_ALWAYSONTOP)
                    viewMainForm.mainPanel.btn_top_Click(null, null);
                //issue 42
                if (IPSConfiguration.AUTOLOAD_FLIGHTPLAN)
                {
                    Thread oThread = new Thread(new ThreadStart(viewMainForm.mainPanel.AsyncFPLoad));
                    oThread.Start();
                }
            }
            catch (FileNotFoundException fnfex)
            {
                //gestione dell'assenza di configurazione
                Log("Configuration file not found: " + fnfex.FileName);
            }

            Log("..Init successifully terminated");
            #endregion
        }

        /// <summary>
        /// Funzione che (alternativamente) mostra e nasconde la finestra di log.
        /// Todo: probabilmente è da spostare in main view
        /// </summary>
        public void ShowHideLog()
        {
            //Lazy building
            if (logForm == null)
                logForm = new LogForm();

            //gestione della visualizzazione
            if (logForm.Visible)
                logForm.Visible = false;
            else
                logForm.Visible = true;
            logForm.Content = log.CurrentLog;
        }

        /// <summary>
        /// Logga opportunamente un messaggio nella console applicativa.
        /// Viene messa nel controller perchè tutte le parti dell'applicazione che richiedono un log
        /// dovrebbero passare di qui.
        /// Come concetto le librerie (blogic) lanciano Exception e la logica (controller) o le viste che
        /// esplodono notificano al controller tramite questo metodo
        /// </summary>
        /// <param name="msg">Messaggio da mettere a log</param>
        public void Log(string msg)
        {
            log.Log(DateTime.Now.ToShortTimeString() + ") " + msg);
            if (logForm.Visible)
            {
                //In questo modo si evita di scrivere su un controllo che sarebbe comunque invisibile
                logForm.Content = log.CurrentLog;
            }
        }

        /// <summary>
        /// Richiesta di connessione a Flight Simulator
        /// </summary>
        /// <returns>true se la connessione riesce, false altrimenti. Non viene lanciata eccezione
        /// perchè questa condizione è considerata lecita</returns>
        public bool Connect()
        {
            try
            {
                if (!flightSim.IsConnected)
                {
                    flightSim.ConnectToFS();
                    return true;
                }
                else
                {
                    Log("FS is already connected");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Richiesta di disconnessione da FS
        /// </summary>
        /// <returns>true se la disconnessione riesce, false altrimenti</returns>
        public bool Disconnect()
        {
            try
            {
                if (flightSim.IsConnected)
                {
                    flightSim.DisconnectToFS();
                    return true;
                }
                else
                {
                    Log("FS is already disconnected");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Inizia il recording dei dati da FS, e quindi attiva la sequenza che porterà a sollevare e gestire
        /// gli eventi provenienti da FS.
        /// Ha come precondizione il fatto che si sia connessi ad FS.
        /// </summary>
        /// <returns></returns>
        public bool StartStopRecording()
        {
            try
            {
                if (flightSim.IsConnected)
                {
                    if (flightSim.IsRecording)
                    {
                        flightSim.StopRecording();
                        Log("Rec stopped");
                        flightSim.FlightSimEvent -= new FSWrapper.FSEventHandler(this.HandleEvent);
                        flightSim.ErrorOccurred -= new FSWrapper.ErrorOccurredHandler(flightSim_ErrorOccurred);
                    }
                    else
                    {
                        flightSim.StartRecording();
                        Log("Rec started");
                        flightSim.FlightSimEvent += new FSWrapper.FSEventHandler(this.HandleEvent);
                        flightSim.ErrorOccurred += new FSWrapper.ErrorOccurredHandler(flightSim_ErrorOccurred);
                        //derivato da issue 63:
                        Thread.Sleep(1200);//per fare in modo che faccia la prima lettura...
                        Log(IvapStatus.Instance.IsRunning ? "detected IVAP running" : "NOT detected IVAP running");
                        //derivato da issue 11:
                        Log("FS version: " + IvapStatus.Instance.FSVersion.ToString());                        
                    }
                    return true;
                }
                else
                {
                    Log("FS not connected");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log(ex.Message);
                Log(ex.StackTrace);
                return false;
            }
        }

        void flightSim_ErrorOccurred(Exception ex)
        {
            Log(ex.Message);
            Log(ex.StackTrace);
        }

        /// <summary>
        /// Dice se è attualmente attivo il polling di dati da FS
        /// </summary>
        public bool IsRecording
        {
            get
            {
                return flightSim.IsRecording;
            }
        }

        /// <summary>
        /// Reperisce il piano di volo dai server di IVAO
        /// </summary>
        /// <returns>true se il reperimento riesce</returns>
        public bool FetchFlightPlan()
        {
            status.Callsign = IPSConfiguration.CALLSIGN;
            status.VirtualAirlineID = IPSConfiguration.VA_ID;

            IvaoFlightPlan fp = null;

            //try/catch inserito per issue 62
            try
            {
                fp = IPSUtils.RetrivePlan(status.Callsign);
            }
            catch (Exception ex)
            {
                Log("Impossibile reperire il piano di volo: " + ex.Message);
                Log(ex.StackTrace);
            }

            if (fp != null)
            {
                status.FlightPlan = fp;
                return true;
            }
            else
                return false;
        }

        public void SaveConfig()
        {
            try
            {
                IPSConfiguration.Save();
                status.Callsign = IPSConfiguration.CALLSIGN;
                status.VirtualAirlineID = IPSConfiguration.VA_ID;
                viewMainForm.mainPanel.Info("Config saved");
                checklistSelectionForm = null;//per issue 89
            }
            catch (Exception ex)
            {
                Log("Error saving config: " + ex.ToString());
                viewMainForm.mainPanel.Error("Error saving config");
            }

        }

        /// <summary>
        /// Metodo fondamentale: è il listener che viene notificato ogni volta che si ricevono dati "freschi"
        /// da FS, quindi è qui che si decide cosa fa l'applazione in risposta a ciascuna situazione
        /// </summary>
        /// <param name="e">L'evento di FS da gestire. Consultare la tassonomia con capostipite FSEvent
        /// per sapere quali sono gli eventi gestibili</param>
        private void HandleEvent(FSEvent e)
        {
            lock (this)
            {
                if (e is PositioningEvent)
                {
                    PositioningEvent pe = (PositioningEvent)e;
                    status.CurrentPosition = pe.Position;
                    if (PositionUpdated != null)
                    {
                        PositionUpdated(pe.Position);
                    }                    
                }
                else if (e is TakeOffEvent)
                {
                    //se sono già airborne o atterrato vuol dire che è un touch 'n' go o un goaround, quindi non faccio nulla
                    if (status.CurrentStatus <= FlightStates.TakeOffTaxi)//il <= inserito per issue 51
                    {
                        status.DepartureTime = e.Timestamp;
                    }
                    status.CurrentStatus = FlightStates.Airborne;
                    //isssue 63
                    if (IPSConfiguration.AUTO_TRASPONDER)
                    {
                        if (IvapStatus.Instance.IsRunning && IvapStatus.Instance.IvapTrasponderIsInStandby)
                        { 
                            //ivap è presente ma il trasponder è in Sierra, quindi lo metto in charlie
                            flightSim.SetTrasponderMode(true);
                        }
                    }
                }
                else if (e is LandingEvent)
                {
                    if (status.CurrentStatus == FlightStates.Airborne)
                    {
                        status.CurrentStatus = FlightStates.Landed;
                        status.ArrivalTime = e.Timestamp;
                        //in questo modo l'ultimo atterraggio è sempre quello che fa fede per l'ora di arrivo
                        //isssue 63:
                        if (IPSConfiguration.AUTO_TRASPONDER)
                        {
                            if (IvapStatus.Instance.IsRunning && !IvapStatus.Instance.IvapTrasponderIsInStandby)
                            {
                                //ivap è presente ma il trasponder è in Sierra, quindi lo metto in charlie
                                flightSim.SetTrasponderMode(false);
                            }
                        }
                    }
                }
                else if (e is EngineStartUpEvent)
                {
                    //se è la prima accensione la considero come quella "buona"
                    if (status.CurrentStatus == FlightStates.Before_Departed)
                    {
                        status.CurrentStatus = FlightStates.Engine_Started;
                        status.DepartureFuel = status.CurrentFuel;
                    }
                }
                else if (e is EngineShutDownEvent)
                {
                    //seconda condizione del primo if è aggiunta per issue 49
                    //se c'è vento l'indicata non scende sotto 1 e quindi non si passa mai nello stato "ai blocchi"
                    if (status.CurrentStatus == FlightStates.OnBlocks || status.CurrentStatus == FlightStates.Landed)
                    {
                        status.CurrentStatus = FlightStates.EngineOff;
                        status.ArrivalFuel = status.CurrentFuel;
                    }
                    else if (status.CurrentStatus == FlightStates.Engine_Started || status.CurrentStatus == FlightStates.TakeOffTaxi)
                    {
                        status.CurrentStatus = FlightStates.Before_Departed;
                    }
                }
                else if (e is StartMovingEvent)
                {
                    if (status.CurrentStatus <= FlightStates.Engine_Started)
                    {
                        status.CurrentStatus = FlightStates.TakeOffTaxi;
                        //issue 54
                        if (status.DepartureFuel == 0)
                            status.DepartureFuel = status.CurrentFuel;
                    }
                    else if (status.CurrentStatus == FlightStates.OnBlocks)
                    {
                        //vuol dire che non ero realmente ai blocchi, ma che mi ero solo arrestato durante il taxi dopo l'atterraggio
                        status.CurrentStatus = FlightStates.Landed;
                    }
                    //negli altri casi è un normale stop durante il rullaggio di partenza o di arrivo
                }
                else if (e is EndMovingEvent)
                {
                    if (status.CurrentStatus == FlightStates.Landed || status.CurrentStatus == FlightStates.OnBlocks)
                    {
                        //la seconda condizione è per evitare di avere problemi se mi fermo durante il taxi dopo l'atterraggio
                        status.CurrentStatus = FlightStates.OnBlocks;
                        //l'ultimo tra on-bock e shutdown motori determina l'ultimo calcolo di fuel
                        status.ArrivalFuel = status.CurrentFuel;
                    }
                }
                else
                    throw new InvalidOperationException("non implementato");

                if (!(e is PositioningEvent))
                {
                    Log(e.GetType().FullName.Substring(e.GetType().FullName.LastIndexOf('.')));
                }
            }

            //rinfresco la view
            viewMainForm.mainPanel.DrawStatus(status);
            utilBar.UpdateView(status);
        }

        public void SpeekCurrentPosition()
        {
            checklistSpeaker.ReadPosition(status.CurrentPosition);
        }

        public void ShowHideMaps()//creato per issue 71
        {
            if (imageViewer == null)
            {
                imageViewer = new ImageViewer();
                imageViewer.Height = Convert.ToInt32(Screen.PrimaryScreen.Bounds.Height * 0.8);
                imageViewer.Width = Convert.ToInt32(Screen.PrimaryScreen.Bounds.Width * 0.8);
            }
            
            if (imageViewer.ImagesPath == null)
            {
                imageViewer.ImagesPath = ImageLoader.FindImages();
            }

            imageViewer.Visible = !imageViewer.Visible;
        }

        public void ShowHideUtilityBar() //creato per issue 87
        {
            if (utilBar == null) utilBar = new UtilityBar();

            if (!utilBar.Visible)
                utilBar.Visible = true;
            else
                utilBar.PressedSelect();
        }

        public void UtilityBarUp()
        {
            utilBar.PressedUp();
        }

        public void UtilityBarDown()
        {
            utilBar.PressedDown();
        }

        /// <summary>
        /// Mostra il messaggio sulla utility bar
        /// </summary>
        /// <param name="message"></param>
        public void ShowMessage(string message)
        {
            if(utilBar != null)
                utilBar.ShowMessage(message);
        }

        public void SpeekChecklistPhase(int phaseNumber)
        {
            if(checklistSelectionForm!=null) checklistSelectionForm.Visible = false;
            Checklist chklst = ChecklistReader.ReadChecklist(IPSConfiguration.CURRENT_CHECKLIST);
            if (chklst != null)
            {
                lastPhaseNumber = phaseNumber;
                checklistSpeaker.ReadPhase(chklst.Phases[phaseNumber]);
            }
            else
                checklistSpeaker.ReadPhase(null);
        }

        public void SpeekChecklistSpeeds()
        {
            try
            {
                Checklist chklst = ChecklistReader.ReadChecklist(IPSConfiguration.CURRENT_CHECKLIST);
                checklistSpeaker.ReadAllSpeeds(chklst);
            }
            catch (Exception)
            {
                checklistSpeaker.ReadAllSpeeds(null);
            }
        }

        public void NextChecklistSelection()
        {
            //CTRL+6 pressed issue 103
            if (checklistSpeaker.IsCurrentlySpeaking() || checklistSpeaker.IsCurrentlyPaused())
            {
                checklistSpeaker.StopSpeaking();
                Thread.Sleep(200);
                ReadNextCheck();
            }
            else
            {
                ReadNextCheck();
            }
        }

        public void ShowHideChecklistSelection()
        {
            //CTRL+1 pressed
            if (checklistSpeaker.IsCurrentlySpeaking() || checklistSpeaker.IsCurrentlyPaused())//issue 66, 77
            {
                checklistSpeaker.StopSpeaking();
                Thread.Sleep(200);
                checklistSpeaker.Speak("canceled");
                return;
            }

            //metodo creato per issue 68
            if (checklistSelectionForm == null)
            {
                checklistSelectionForm = new TransparentChoiseForm();
                LoadChecklistPhase();
                checklistSelectionForm.SelectedEvent += new TransparentChoiseForm.SelectedIndexHandler(this.SpeekChecklistPhase);
            }
            checklistSelectionForm.ChooseTitle = "Chose checklist phase to be readed:";
            checklistSelectionForm.Visible = !checklistSelectionForm.Visible;
            if (checklistSelectionForm.Visible) checklistSelectionForm.Activate();
        }

        public void PauseResumeSpeaking()
        {
            //CTRL+3 pressed
            if (checklistSpeaker.IsCurrentlySpeaking())
                checklistSpeaker.PauseSpeaking();
            else
                if (checklistSpeaker.IsCurrentlyPaused())
                    checklistSpeaker.ResumeSpeaking();
                else
                {
                    ReadNextCheck();
                }
        }

        public delegate void PositionEventHandler(AircraftPosition pos);
        /// <summary>
        /// Evento a cui sottoscriversi per ricevere tutti gli eventi generati dall'applicazione a partire
        /// dalle letture fatte sulle FSUIPC. Il listner designato a livello di progettazione è IPSController
        /// </summary>
        public event PositionEventHandler PositionUpdated;

        private void LoadChecklistPhase()
        {
            Checklist cklst = ChecklistReader.ReadChecklist(IPSConfiguration.CURRENT_CHECKLIST);
            string[] phases = new string[cklst.Phases.Count];
            int i = 0;
            foreach (ChecklistPhase phase in cklst.Phases)
            {
                phases[i++] = phase.PhaseName;
            }
            if (checklistSelectionForm == null)
            {
                checklistSelectionForm = new TransparentChoiseForm();
            }
            checklistSelectionForm.AvailableChooses = phases;
        }

        private void ReadNextCheck()
        {
            if (lastPhaseNumber < 0)
            {
                //è la prima volta
                SpeekChecklistPhase(0);
            }
            else
            {
                //non è la prima volta. IL try mi protegge dall'overflow del contatore se ho finito le fasi ricomincio dalla prima
                try
                {
                    SpeekChecklistPhase(lastPhaseNumber + 1);
                }
                catch
                {
                    SpeekChecklistPhase(0);
                }
            }
        }

    }
}
