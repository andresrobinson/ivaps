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
        public double Heading { get; set; }
        public double Speed { get; set; }
        public DateTime Timestamp { get; set; }
        public double AvailableFuel { get; set; }
    }
}
