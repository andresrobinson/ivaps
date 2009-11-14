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
    public class IvaoFlightPlan
    {
        public string Route { get; set; }
        public Airport Departure { get; set; }
        public Airport Arrival { get; set; }
        public Airport Alternate { get; set; }
        public string FlightType { get; set; }
        public string Aircraft { get; set; }
    }
}
