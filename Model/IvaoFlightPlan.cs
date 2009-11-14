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
    /// Data Object che rappresenta un piano di volo, nell'accezione IVAO
    /// </summary>
    public class IvaoFlightPlan
    {
        public string Route { get; set; }
        public Airport Departure { get; set; }
        public Airport Arrival { get; set; }
        public Airport Alternate { get; set; }
        /// <summary>
        /// V per VFR, I per IFR, etc.
        /// </summary>
        public string FlightType { get; set; }
        /// <summary>
        /// Codice ICAO dell'aereomobile
        /// </summary>
        public string Aircraft { get; set; }
    }
}
