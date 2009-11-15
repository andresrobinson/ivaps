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
using Castellari.IVaPS.BLogic;

namespace Castellari.IVaPS.Model
{
    /// <summary>
    /// Data Object rappresentante un intero volo. Si tratta dello stato vero e proprio dell'applicazione e vuole
    /// rappresentare uno a uno un intero volo
    /// </summary>
    public class FlightStatus
    {
        private const int INITIAL_FLIGHTLOG_CAPACITY = 1000;
     
        /// <summary>
        /// Posizione corrente dell'aereomobile
        /// </summary>
        private AircraftPosition currentPosition = null;
        /// <summary>
        /// History delle posizioni assunte dall'aereomobile nel tempo. AD OGGI NON UTILIZZATO (NdF 20091114)
        /// </summary>
        private List<AircraftPosition> flightLog = null;
        /// <summary>
        /// Stato corrente del volo, per i dettagli sulla sequenza vedere la documentazione della enum
        /// </summary>
        public FlightStates CurrentStatus { get; set; }
        /// <summary>
        /// Distanza percorsa dall'inizio del recording espressa in nm
        /// </summary>
        public double Distance { get; set; }
        /// <summary>
        /// Massima altitune, espressa in ft, raggiunta durante il volo
        /// </summary>
        public double MaxAltitude { get; set; }
        /// <summary>
        /// Velocità massima (GS) raggiunta durante il volo, espressa in nodi
        /// </summary>
        public double MaxSpeed { get; set; }
        /// <summary>
        /// Piano di volo
        /// </summary>
        public IvaoFlightPlan FlightPlan { get; set; }
        /// <summary>
        /// Ora di partenza REALE. Si tratta dell'ora in cui l'applicazione ha gestito l'ultimo TakeOffEvent
        /// </summary>
        public DateTime DepartureTime { get; set; }
        /// <summary>
        /// Ora di arrivo REALE. Si tratta dell'ora in cui l'applicazione ha gestito l'ultimo LandingEvent
        /// </summary>
        public DateTime ArrivalTime { get; set; }
        /// <summary>
        /// Ora corrente nel tempo del simulatore
        /// </summary>
        public DateTime CurrentSimTime { get; set; }
        /// <summary>
        /// Il callsign corrente del pilota
        /// </summary>
        public string Callsign { get; set; }
        /// <summary>
        /// ID della virtal airlinecorrente
        /// </summary>
        public string VirtualAirlineID { get; set; }
        /// <summary>
        /// Carburante alla partenza in galloni
        /// </summary>
        public double DepartureFuel { get; set; }
        /// <summary>
        /// Carburante all'arrivo in galloni
        /// </summary>
        public double ArrivalFuel { get; set; }
        /// <summary>
        /// Carburante corrente in galloni
        /// </summary>
        public double CurrentFuel { get; set; }

        /// <summary>
        /// Quando viene settata dall'esterno una nuova current position, questa viene
        /// automaticamente aggiunta anche al FlightLog
        /// </summary>
        public AircraftPosition CurrentPosition 
        { 
            get
            {
                return currentPosition;
            }
            set
            {
                double percurredDistance = 0;
                if (currentPosition != null)
                    percurredDistance = IPSUtils.CalulateDistance(currentPosition, value);

                Distance += percurredDistance;
                currentPosition = value;
                //per ora commento per evitare che sbraghi la RAM, poi ci penserò
                //this.FlightLog.Add(currentPosition);
                if (currentPosition.Speed > MaxSpeed)
                    MaxSpeed = currentPosition.Speed;
                if (currentPosition.Altitude > MaxAltitude)
                    MaxAltitude = currentPosition.Altitude;

                CurrentFuel = currentPosition.AvailableFuel;
            }
        }

        /// <summary>
        /// History delle posizioni assunte dall'aereomobile nel tempo. AD OGGI NON UTILIZZATO (NdF 20091114)
        /// </summary>
        public List<AircraftPosition> FlightLog 
        {
            get
            {
                return flightLog;
            }
        }

        #region costruttori
        public FlightStatus()
        {
            flightLog = new List<AircraftPosition>(INITIAL_FLIGHTLOG_CAPACITY);
            Distance = 0;
            MaxAltitude = 0;
            MaxSpeed = 0;
            DepartureTime = DateTime.MinValue;
            ArrivalTime = DateTime.MinValue;
            Callsign = "";
            VirtualAirlineID = "";
            FlightPlan = null;
            CurrentStatus = FlightStates.Before_Departed;
        }
        #endregion
    }
}
