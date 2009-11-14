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
    /// Data Object rappresentante un aereoporto. Ereditando da GeoPosition può avere anche una location
    /// geografica
    /// </summary>
    public class Airport : GeoPosition
    {
        public string ICAOCode { get; set; }
    }
}
