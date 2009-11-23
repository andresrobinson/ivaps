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
        public double Speed { get; set; }
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
        /// Torna true se l'aereomobile è in movimento
        /// </summary>
        public bool IsMoving
        {
            get 
            {
                if (Speed != double.NaN)
                    return Speed > 0;
                else
                    return false;
            }
        }


    }
}
