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

namespace Castellari.IVaPS.Model
{
    /// <summary>
    /// Bean rappresentante la posizione istantanea di un aereomobile
    /// </summary>
    public class AircraftPosition : GeoPosition
    {
        /// <summary>
        /// Heading, in gradi, della prua dell'aereomobile, 0=N, 90=E, 180=S, 270=O
        /// </summary>
        public double Heading { get; set; }
        /// <summary>
        /// Altezza dal suolo, espressa in metri
        /// </summary>
        public double RadioAltitude { get; set; }
        /// <summary>
        /// Velocità al suolo (ground speed) in nodi
        /// </summary>
        public double GroundSpeed { get; set; }
        /// <summary>
        /// Velocità reale all'aria (TAS), espressa in nodi
        /// </summary>
        public double TrueAirspeedSpeed { get; set; }
        /// <summary>
        /// Velocità indicata (IAS), espressa in nodi
        /// </summary>
        public double IndicatedAirspeedSpeed { get; set; }
        /// <summary>
        /// Istande del rilevamento della posizione
        /// </summary>
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Carburante caricato a bordo nel momento del campionamento, espresso in Galloni
        /// </summary>
        public double AvailableFuel { get; set; }
        /// <summary>
        /// Torna true se l'aereo è in volo
        /// </summary>
        public bool IsAirborne { get; set; }
        /// <summary>
        /// Torna true se almeno un motore dell'aereomobile è acceso
        /// </summary>
        public bool IsEngineStarted { get; set; }
        /// <summary>
        /// Valore del QNH letto sull'altimetro
        /// </summary>
        public double QNH { get; set; }
        /// <summary>
        /// Frequenza del ricevitore VOR numero 1
        /// </summary>
        public double Nav1 { get; set; }
        /// <summary>
        /// Offset rispetto al localizzatore sull'OBS di NAV1: da -127 a 127
        /// </summary>
        public short Nav1Localizer { get; set; }
        /// <summary>
        /// Offset rispetto al glide sull'OBS di NAV1: da -127 a 127
        /// </summary>
        public short Nav1Glide { get; set; }
        /// <summary>
        /// Heading dell'OBS di NAV1: da 0 a 359
        /// </summary>
        public int Nav1OBS { get; set; }
        /// <summary>
        /// La radiale corrente in gradi (0-359) del VOR 1
        /// </summary>
        public double Nav1Radial { get; set; }
        /// <summary>
        /// Distanza in nm dal DME di NAV1
        /// </summary>
        public double Nav1DME { get; set; }
        /// <summary>
        /// Heading settato sul nottolino dell'HSI, in gradi 0-359
        /// </summary>
        public int AutopilotHeading { get; set; }
        /// <summary>
        /// La percentuale 0-100 di throttle (in realtà letto sul throttle dell'engine 1, ignorando gli inversori
        /// </summary>
        public int ThrottlePercentage { get; set; }
        /// <summary>
        /// Torna true se l'aereomobile è in movimento
        /// </summary>
        public bool IsMoving
        {
            get 
            {
                if (GroundSpeed != double.NaN)
                    return GroundSpeed > 0;
                else
                    return false;
            }
        }


    }
}
