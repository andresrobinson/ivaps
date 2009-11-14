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
    /// Capostipite della tassonomia degli eventi provenienti da Flight Simulator
    /// </summary>
    public class FSEvent
    {
        /// <summary>
        /// Timestamp di generazione dell'evento, ora del PC
        /// </summary>
        public DateTime Timestamp { get; set; }
    }
}
