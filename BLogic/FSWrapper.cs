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
using System.Timers;

using FSUIPC;

using Castellari.IVaPS.Model;

namespace Castellari.IVaPS.BLogic
{
    /// <summary>
    /// Rappresenta il wrapper dell'intero Flight Simulator: quando l'applicazione necessita di colloquiare con FS
    /// lo fa sempre e solo attraverso questa entità.
    /// 
    /// Il principio di funzionamento è il seguente:
    /// 1. una volta connesso faccio un pollingo ogni TIMER_ELAPSED_MILLISECONDS alle FSUIPC.dll
    /// 2. i dati mi vengono scritti nella variabili di tipo Offset<> 
    /// 3. leggo i dati da li, li monto in oggetti del dominio applicativo e sollevo gli eventi adeguati
    /// 
    /// Per aggiungere quindi un nuovo dato da leggere:
    /// a. individuare il falore HEX dell'offset e metterlo nelle "costanti di OFFSET"
    /// b. verificare dove e come inserire nelle classi di modello applicativo il nuovo dato
    /// c. crea la variabile corrispondente di tipo Offset<>
    /// d. nel metodo "TickHandle" metti il valore dell'Offset<>.Value nella variabile di oggetto applicativo
    /// e. Solleva l'evento corrispondente.
    /// </summary>
    public class FSWrapper
    {
        #region costanti di OFFSET di FSUIPC
        private const int OFFSET_GS = 0x02B4;
        private const int OFFSET_TAS = 0x02B8;
        private const int OFFSET_LAT = 0x0560;
        private const int OFFSET_LON = 0x0568;
        private const int OFFSET_ALT = 0x0574;
        private const int OFFSET_HDG = 0x0580;
        private const int OFFSET_AIRBORNE = 0x0366;
        private const int RADIO_ALTITUDE= 0x31E4;
        
        private const int OFFSET_IVAP_DETECTED = 0x7b80;//unused currently
        private const int OFFSET_IVAP_TRASPONDER_STATUS = 0x7B91;

        private const int OFFSET_FSVERSION = 0x3308;

        private const int OFFSET_FUEL_CONTENT_CENTER = 0x0B74;
        private const int OFFSET_FUEL_CAPACITY_CENTER = 0x0B78;

        private const int OFFSET_FUEL_CONTENT_LEFTMAIN = 0x0B7C;
        private const int OFFSET_FUEL_CAPACITY_LEFTMAIN = 0x0B80;

        private const int OFFSET_FUEL_CONTENT_LEFTAUX = 0x0B84;
        private const int OFFSET_FUEL_CAPACITY_LEFTAUX = 0x0B88;

        private const int OFFSET_FUEL_CONTENT_LEFTTIP = 0x0B8C;
        private const int OFFSET_FUEL_CAPACITY_LEFTTIP = 0x0B90;

        private const int OFFSET_FUEL_CONTENT_RIGHTMAIN = 0x0B94;
        private const int OFFSET_FUEL_CAPACITY_RIGHTMAIN = 0x0B98;

        private const int OFFSET_FUEL_CONTENT_RIGHTAUX = 0x0B9C;
        private const int OFFSET_FUEL_CAPACITY_RIGHTAUX = 0x0BA0;

        private const int OFFSET_FUEL_CONTENT_RIGHTTIP = 0x0BA4;
        private const int OFFSET_FUEL_CAPACITY_RIGHTTIP = 0x0BA8;

        #endregion

        #region Variabili di tipo Offset<>
        private Offset<int> groundspeed = new Offset<int>(OFFSET_GS);//issue 50
        private Offset<int> airspeed = new Offset<int>(OFFSET_TAS);//corretto per issue 32
        private Offset<long> latitude = new Offset<long>(OFFSET_LAT);
        private Offset<long> longitude = new Offset<long>(OFFSET_LON);
        private Offset<int> altitude = new Offset<int>(OFFSET_ALT);
        private Offset<int> heading = new Offset<int>(OFFSET_HDG);
        private Offset<short> airborne = new Offset<short>(OFFSET_AIRBORNE);
        private Offset<int> radioAlt = new Offset<int>(RADIO_ALTITUDE);
        
        private Offset<byte> ivapDetected = new Offset<byte>(OFFSET_IVAP_DETECTED);
        private Offset<byte> ivapTrasponder = new Offset<byte>(OFFSET_IVAP_TRASPONDER_STATUS);

        private Offset<short> fsversion = new Offset<short>(OFFSET_FSVERSION);

        private Offset<int> fuelCap1 = new Offset<int>(OFFSET_FUEL_CAPACITY_CENTER);
        private Offset<int> fuelCap2 = new Offset<int>(OFFSET_FUEL_CAPACITY_LEFTMAIN);
        private Offset<int> fuelCap3 = new Offset<int>(OFFSET_FUEL_CAPACITY_LEFTAUX);
        private Offset<int> fuelCap4 = new Offset<int>(OFFSET_FUEL_CAPACITY_LEFTTIP);
        private Offset<int> fuelCap5 = new Offset<int>(OFFSET_FUEL_CAPACITY_RIGHTMAIN);
        private Offset<int> fuelCap6 = new Offset<int>(OFFSET_FUEL_CAPACITY_RIGHTAUX);
        private Offset<int> fuelCap7 = new Offset<int>(OFFSET_FUEL_CAPACITY_RIGHTTIP);

        private Offset<int> fuelQty1 = new Offset<int>(OFFSET_FUEL_CONTENT_CENTER);
        private Offset<int> fuelQty2 = new Offset<int>(OFFSET_FUEL_CONTENT_LEFTMAIN);
        private Offset<int> fuelQty3 = new Offset<int>(OFFSET_FUEL_CONTENT_LEFTAUX);
        private Offset<int> fuelQty4 = new Offset<int>(OFFSET_FUEL_CONTENT_LEFTTIP);
        private Offset<int> fuelQty5 = new Offset<int>(OFFSET_FUEL_CONTENT_RIGHTMAIN);
        private Offset<int> fuelQty6 = new Offset<int>(OFFSET_FUEL_CONTENT_RIGHTAUX);
        private Offset<int> fuelQty7 = new Offset<int>(OFFSET_FUEL_CONTENT_RIGHTTIP);
        #endregion

        /// <summary>
        /// Flag per sapere se si è correntemente connessi o meno a FS
        /// </summary>
        private bool connected = false;

        /// <summary>
        /// Flag per gestire un minimo di isteresi nella gestione del consumo di carburante: issue 44
        /// </summary>
        private bool isFirsPosWithNoFuelFlow = true;

        /// <summary>
        /// Questo timer è quello che gestisce il watch-dog per il polling a FS
        /// </summary>
        private Timer timer = new Timer(IPSConfiguration.TIMER_ELAPSED_MILLISECONDS);

        /// <summary>
        /// Costruttore
        /// </summary>
        public FSWrapper()
        {
            LastPosition = new AircraftPosition();
            LastPosition.Altitude = double.NaN;
            LastPosition.RadioAltitude = double.NaN;
            LastPosition.AvailableFuel = double.NaN;
            LastPosition.Heading = double.NaN;
            LastPosition.IsAirborne = false;
            LastPosition.IsEngineStarted = false;
            LastPosition.Latitude = double.NaN;
            LastPosition.Longitude = double.NaN;
            LastPosition.TrueAirspeedSpeed = double.NaN;
            LastPosition.GroundSpeed = double.NaN;
            LastPosition.Timestamp = DateTime.Now;
        }

        /// <summary>
        /// Ultima posizione letta dalle FSUIPC
        /// </summary>
        public AircraftPosition LastPosition { get; set; }

        public delegate void FSEventHandler(FSEvent fsEvent);
        /// <summary>
        /// Evento a cui sottoscriversi per ricevere tutti gli eventi generati dall'applicazione a partire
        /// dalle letture fatte sulle FSUIPC. Il listner designato a livello di progettazione è IPSController
        /// </summary>
        public event FSEventHandler FlightSimEvent;

        public delegate void ErrorOccurredHandler(Exception ex);

        /// <summary>
        /// Evento sollevato ogni qual volta si verifichi un errore nel modulo. Registrandosi a questo evento
        /// si è quindi notificati anche di tutti gli errori che possono occorrere durante il normale
        /// funzionamento del demone (timer) interno.
        /// </summary>
        public event ErrorOccurredHandler ErrorOccurred;

        /// <summary>
        /// Metodo per attivare la connessione ad FSUIPC
        /// </summary>
        public void ConnectToFS()
        {
            FSUIPCConnection.Open();
            connected = true;
        }

        /// <summary>
        /// Metodo per disattivare la connessione ad FSUIPC
        /// </summary>
        public void DisconnectToFS()
        {
            FSUIPCConnection.Close();
            connected = false;
        }

        /// <summary>
        /// Dice se il wrapper è correntemente connesso con FSUIPC
        /// </summary>
        public bool IsConnected
        {
            get 
            {
                return connected;
            }
        }

        /// <summary>
        /// Attiva un timer che fa "polling" dei parametri delle FSUIPC ogni TIMER_ELAPSED_MILLISECONDS
        /// </summary>
        public void StartRecording()
        {
            timer.Elapsed += new ElapsedEventHandler(this.TickHandle);
            timer.Enabled = true;
        }

        /// <summary>
        /// Disattiva il timer che governa il polling dello stato alle FSUIPC
        /// </summary>
        public void StopRecording()
        {
            timer.Enabled = false;
            timer.Elapsed -= new ElapsedEventHandler(this.TickHandle);
        }

        /// <summary>
        /// Torna true se il polling è attivo
        /// </summary>
        public bool IsRecording
        {
            get 
            {
                return timer.Enabled;
            }
        }

        /// <summary>
        /// Permette all'esterno di forzare una lettura delle FSUIPC
        /// </summary>
        public void ForceReading()
        {
            TickHandle(null, null);
        }

        /// <summary>
        /// Cambia lo stato del trasponder
        /// </summary>
        /// <param name="switchInCharlieMode">true per metterlo in charlie, false per riportarlo in Sierra</param>
        public void SetTrasponderMode(bool switchInCharlieMode)
        {
            //metodo per issue 63
            ivapTrasponder.Value = switchInCharlieMode ? (byte)0 : (byte)1;
            //da verificare se serve FSUIPCConnection.Process();
        }

        /// <summary>
        /// Gestore interno del watch dog sulle FSUIPC. In caso di errore sorreva un ErrorOccurredEvent
        /// </summary>
        private void TickHandle(object sender, ElapsedEventArgs e)
        {
            try
            {
                PositioningEvent toBeRaised = new PositioningEvent();
                AircraftPosition currentPosition = new AircraftPosition();

                FSUIPCConnection.Process();

                //issue 63
                #region gestione di ivap
                IvapStatus.Instance.IsRunning = ((byte)ivapDetected.Value == 1);
                if (IvapStatus.Instance.IsRunning)
                {
                    IvapStatus.Instance.IvapTrasponderIsInStandby = ((byte)ivapTrasponder.Value == 1);
                }
                else IvapStatus.Instance.IvapTrasponderIsInStandby = true;
                #endregion

                //issue 11
                #region versione FS
                switch ((short)ivapTrasponder.Value)
                {
                    case 7:
                        IvapStatus.Instance.FSVersion = FlightSimulatorVersion.FS2004;
                        break;
                    case 9:
                        IvapStatus.Instance.FSVersion = FlightSimulatorVersion.FSX;
                        break;
                    default:
                        IvapStatus.Instance.FSVersion = FlightSimulatorVersion.Altro;
                        break;
                }
                #endregion

                //ground speed
                currentPosition.GroundSpeed = ((double)groundspeed.Value / 65536d) * 1.943844492;//Knots
                //airspeed
                currentPosition.TrueAirspeedSpeed = ((double)airspeed.Value / 128);//issue 38
                //latitude
                currentPosition.Latitude = (double)latitude.Value * 90d / (10001750d * 65536d * 65536d);
                //longitude
                currentPosition.Longitude = (double)longitude.Value * 360d / (65536d * 65536d * 65536d * 65536d);
                //altitude
                currentPosition.Altitude = (int)altitude.Value * 3.28d;
                //radio altitude
                currentPosition.RadioAltitude = (double)radioAlt.Value / 65536;
                //heading
                currentPosition.Heading = heading.Value * 360d / (65536d * 65536d);
                if (currentPosition.Heading < 0)
                    currentPosition.Heading = 360 + currentPosition.Heading;

                //fuel
                currentPosition.AvailableFuel = 0;
                currentPosition.AvailableFuel += fuelCap1.Value * ((double)(fuelQty1.Value) / (128 * 65536));
                currentPosition.AvailableFuel += fuelCap2.Value * ((double)(fuelQty2.Value) / (128 * 65536));
                currentPosition.AvailableFuel += fuelCap3.Value * ((double)(fuelQty3.Value) / (128 * 65536));
                currentPosition.AvailableFuel += fuelCap4.Value * ((double)(fuelQty4.Value) / (128 * 65536));
                currentPosition.AvailableFuel += fuelCap5.Value * ((double)(fuelQty5.Value) / (128 * 65536));
                currentPosition.AvailableFuel += fuelCap6.Value * ((double)(fuelQty6.Value) / (128 * 65536));
                currentPosition.AvailableFuel += fuelCap7.Value * ((double)(fuelQty7.Value) / (128 * 65536));

                toBeRaised.Position = currentPosition;
                //sollevo l'evento
                FlightSimEvent(toBeRaised);

                //gestione stato airborne
                currentPosition.IsAirborne = (airborne.Value == 0) || /*issue 36*/(currentPosition.RadioAltitude > 10);
                if (!LastPosition.IsAirborne && currentPosition.IsAirborne)
                {
                    //decollato
                    FSEvent to = new TakeOffEvent();
                    FlightSimEvent(to);
                }
                else if (LastPosition.IsAirborne && !currentPosition.IsAirborne)
                {
                    //atterrato
                    FSEvent ldg = new LandingEvent();
                    FlightSimEvent(ldg);
                }

                //gestione dello stato del motore (ed invio eventi associati) issue 29
                currentPosition.IsEngineStarted = currentPosition.AvailableFuel < LastPosition.AvailableFuel;

                if (!LastPosition.IsEngineStarted && currentPosition.IsEngineStarted)
                {
                    //il motore si è acceso
                    FSEvent evt = new EngineStartUpEvent();
                    FlightSimEvent(evt);

                }
                else if (LastPosition.IsEngineStarted && !currentPosition.IsEngineStarted)
                {
                    if (isFirsPosWithNoFuelFlow)
                    {
                        //visto che è la prima volta che questo accade, lascio una tolleranza di un giro
                        //issue 44
                        isFirsPosWithNoFuelFlow = false;
                        currentPosition.IsEngineStarted = true;
                    }
                    else
                    {
                        //il motore si è spento davvero
                        FSEvent evt = new EngineShutDownEvent();
                        FlightSimEvent(evt);
                        isFirsPosWithNoFuelFlow = true;
                    }
                }

                //gestione del movimento (ed invio eventi associati) issue 29
                //l'1 invece dello 0 è dovuto a potenziali errori di arrotondamento nel calcolo della velocità
                if (LastPosition.GroundSpeed <= 1 && currentPosition.GroundSpeed > 1)
                {
                    //inizia a muoversi
                    FSEvent evt = new StartMovingEvent();
                    FlightSimEvent(evt);
                }
                else if (LastPosition.GroundSpeed >= 1 && currentPosition.GroundSpeed < 1)
                {
                    //si ferma
                    FSEvent evt = new EndMovingEvent();
                    FlightSimEvent(evt);
                }

                LastPosition = currentPosition;
            }
            catch (Exception ex)
            {
                ErrorOccurred(ex);
            }
        }

    }
}
