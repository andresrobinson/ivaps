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
            flightSim.Controller = this;
            status = new FlightStatus();
            viewMainForm.mainPanel.SetStatus(status);
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
            log.Log(DateTime.Now.ToShortTimeString() + "] " +  msg);
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
                        flightSim.FlightSimEvent -= new FSWrapper.FSEventHandler(this.HandleEvent);
                    }
                    else
                    {
                        flightSim.StartRecording();
                        flightSim.FlightSimEvent += new FSWrapper.FSEventHandler(this.HandleEvent);
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
                return false;
            }
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
        /// <param name="callsign">il callsign del pilota</param>
        /// <param name="virtualAirlineCode">il codice della virtual airline</param>
        /// <returns>true se il reperimento riesce</returns>
        public bool FetchFlightPlan(string callsign, string virtualAirlineCode)
        {
            status.Callsign = callsign;
            status.VirtualAirlineID = virtualAirlineCode;

            IvaoFlightPlan fp = IPSUtils.RetrivePlan(status.Callsign);
            if (fp != null)
            {
                status.FlightPlan = fp;
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Metodo fondamentale: è il listener che viene notificato ogni volta che si ricevono dati "freschi"
        /// da FS, quindi è qui che si decide cosa fa l'applazione in risposta a ciascuna situazione
        /// </summary>
        /// <param name="e">L'evento di FS da gestire. Consultare la tassonomia con capostipite FSEvent
        /// per sapere quali sono gli eventi gestibili</param>
        private void HandleEvent(FSEvent e)
        {
            if (e is PositioningEvent)
            {
                PositioningEvent pe = (PositioningEvent)e;
                status.CurrentPosition = pe.Position;
            }
            else if (e is TakeOffEvent)
            {
                //se sono già airborne o atterrato vuol dire che è un touch 'n' go o un goaround, quindi non faccio nulla
                if (status.CurrentStatus == FlightStates.TakeOffTaxi)
                {
                    status.DepartureTime = e.Timestamp;
                }
                status.CurrentStatus = FlightStates.Airborne;
            }
            else if (e is LandingEvent)
            {
                if (status.CurrentStatus == FlightStates.Airborne)
                {
                    status.CurrentStatus = FlightStates.Landed;
                    status.ArrivalTime = e.Timestamp;
                    //in questo modo l'ultimo atterraggio è sempre quello che fa fede per l'ora di arrivo
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
                if (status.CurrentStatus == FlightStates.OnBlocks)
                {
                    status.CurrentStatus = FlightStates.EngineOff;
                    status.ArrivalFuel = status.CurrentFuel; 
                }
                else if(status.CurrentStatus == FlightStates.Engine_Started || status.CurrentStatus == FlightStates.TakeOffTaxi)
                {
                    status.CurrentStatus = FlightStates.Before_Departed;
                }
            }
            else if (e is StartMovingEvent)
            {
                if (status.CurrentStatus == FlightStates.Before_Departed || status.CurrentStatus == FlightStates.Engine_Started)
                {
                    status.CurrentStatus = FlightStates.TakeOffTaxi;
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


            //rinfresco la view
            viewMainForm.mainPanel.DrawStatus(status);
        }
        
    }
}
