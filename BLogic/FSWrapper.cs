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

using Castellari.IVaPS.Control;
using Castellari.IVaPS.Model;

namespace Castellari.IVaPS.BLogic
{
    /// <summary>
    /// Rappresenta il wrapping dell'intero Flight Simulator: quando l'applicazione necessita di colloquiare con FS
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
        private const int OFFSET_LAT = 0x0560;
        private const int OFFSET_LON = 0x0568;
        private const int OFFSET_ALT = 0x0574;
        private const int OFFSET_HDG = 0x0580;
        private const int OFFSET_AIRBORNE = 0x0366;
        
        private const int OFFSET_IVAP_DETECTED = 0x7b80;//unused currently

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
        private Offset<int> airspeed = new Offset<int>(OFFSET_GS);
        private Offset<long> latitude = new Offset<long>(OFFSET_LAT);
        private Offset<long> longitude = new Offset<long>(OFFSET_LON);
        private Offset<int> altitude = new Offset<int>(OFFSET_ALT);
        private Offset<int> heading = new Offset<int>(OFFSET_HDG);
        private Offset<short> airborne = new Offset<short>(OFFSET_AIRBORNE);
        private Offset<byte> ivapDetected = new Offset<byte>(OFFSET_IVAP_DETECTED);

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
        /// Questo timer è quello che gestisce il watch-dog per il polling a FS
        /// </summary>
        private Timer timer = new Timer(IPSConfiguration.TIMER_ELAPSED_MILLISECONDS);
        /// <summary>
        /// Flag che dice se in questo momento l'aereomobile è o meno in volo
        /// </summary>
        private bool isAirborne = false;


        public delegate void FSEventHandler(FSEvent fsEvent);
        /// <summary>
        /// Evento a cui sottoscriversi per ricevere tutti gli eventi generati dall'applicazione a partire
        /// dalle letture fatte sulle FSUIPC. Il listner designato a livello di progettazione è IPSController
        /// </summary>
        public event FSEventHandler FlightSimEvent;

        /// <summary>
        /// Riferimento al controllo necessario unicamente per il logging. Questa deroga alla normale logica che
        /// le librerie non loggano è dovuto al fatto che il thread è asincrono e le eccezioni andrebbero perse.
        /// </summary>
        public IPSController Controller { get; set; }
        
        public void ConnectToFS()
        {
            FSUIPCConnection.Open();
            connected = true;
        }

        public void DisconnectToFS()
        {
            FSUIPCConnection.Close();
            connected = false;
        }

        public bool IsConnected
        {
            get 
            {
                return connected;
            }
        }

        public void StartRecording()
        {
            timer.Elapsed += new ElapsedEventHandler(this.TickHandle);
            timer.Enabled = true;
            Controller.Log("Rec started");
        }

        public void StopRecording()
        {
            timer.Enabled = false;
            timer.Elapsed -= new ElapsedEventHandler(this.TickHandle);
            Controller.Log("Rec stopped");
        }

        public bool IsRecording
        {
            get 
            {
                return timer.Enabled;
            }
        }

        private void TickHandle(object sender, ElapsedEventArgs e)
        {
            try
            {
                PositioningEvent toBeRaised = new PositioningEvent();
                AircraftPosition pos = new AircraftPosition();
                toBeRaised.Timestamp = DateTime.Now;

                FSUIPCConnection.Process();
                //airspeed
                pos.Speed = ((double)airspeed.Value / 65536d) * 1.943844492;//Knots
                //latitude
                pos.Latitude = (double)latitude.Value * 90d / (10001750d * 65536d * 65536d);
                //longitude
                pos.Longitude = (double)longitude.Value * 360d / (65536d * 65536d * 65536d * 65536d);
                //altitude
                pos.Altitude = (int)altitude.Value * 3.28d;
                //heading
                pos.Heading = (double)heading.Value * 360d / (65536d * 65536d);
                if(pos.Heading < 0)
                    pos.Heading = 360 + pos.Heading;

                //fuel
                pos.AvailableFuel = 0;
                pos.AvailableFuel += fuelCap1.Value * ((double)(fuelQty1.Value) / (128 * 65536));
                pos.AvailableFuel += fuelCap2.Value * ((double)(fuelQty2.Value) / (128 * 65536));
                pos.AvailableFuel += fuelCap3.Value * ((double)(fuelQty3.Value) / (128 * 65536));
                pos.AvailableFuel += fuelCap4.Value * ((double)(fuelQty4.Value) / (128 * 65536));
                pos.AvailableFuel += fuelCap5.Value * ((double)(fuelQty5.Value) / (128 * 65536));
                pos.AvailableFuel += fuelCap6.Value * ((double)(fuelQty6.Value) / (128 * 65536));
                pos.AvailableFuel += fuelCap7.Value * ((double)(fuelQty7.Value) / (128 * 65536));

                toBeRaised.Position = pos;
                //sollevo l'evento
                FlightSimEvent(toBeRaised);

                //airborne
                bool isNowAirborne = (airborne.Value == 0);
                if (!isAirborne && isNowAirborne)
                {
                    //decollato
                    FSEvent to = new TakeOffEvent();
                    to.Timestamp = DateTime.Now;
                    FlightSimEvent(to);
                    isAirborne = true;
                }
                else if (isAirborne && !isNowAirborne)
                {
                    //atterrato
                    FSEvent ldg = new LandingEvent();
                    ldg.Timestamp = DateTime.Now;
                    FlightSimEvent(ldg);
                    isAirborne = false;
                }
            }
            catch (Exception ex)
            {
                Controller.Log(ex.Message);
                Controller.Log(ex.StackTrace);
            }
        }

    }
}
