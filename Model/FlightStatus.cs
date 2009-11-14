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
    /// Bean rappresentante un intero volo
    /// </summary>
    public class FlightStatus
    {
        private const int INITIAL_FLIGHTLOG_CAPACITY = 10000;
        private AircraftPosition currentPosition = null;
        private List<AircraftPosition> flightLog = null;

        /// <summary>
        /// Distanza percorsa dall'inizio del recording espressa in nm
        /// </summary>
        public double Distance { get; set; }
        /// <summary>
        /// Massima altitune, espressa in ft, raggiunta durante il volo
        /// </summary>
        public double MaxAltitude { get; set; }
        /// <summary>
        /// Velocità massima raggiunta durante il volo, espressa in nodi
        /// </summary>
        public double MaxSpeed { get; set; }
        public string FlightPlan { get; set; }
        public Airport Departure { get; set; }
        public DateTime DepartureTime { get; set; }
        public Airport Arrival { get; set; }
        public DateTime ArrivalTime { get; set; }
        public DateTime CurrentSimTime { get; set; }
        public Airport Alternate { get; set; }
        public string FlightType { get; set; }
        public string Callsign { get; set; }
        public string VirtualAirlineID { get; set; }
        public string Aircraft { get; set; }
        public int TotalFuelAllowed { get; set; }
        public double DeparturenFuel { get; set; }
        public double ArrivalFuel { get; set; }
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
            FlightPlan = "";
            FlightType = "";
            Aircraft = "";
        }
        #endregion
    }
}
