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
    /// Evento sollevato ad ogni posizionaento dell'aereomobile
    /// </summary>
    public class PositioningEvent : FSEvent
    {
        public AircraftPosition Position { get; set; }
    }
}
