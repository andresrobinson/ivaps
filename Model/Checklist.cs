using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Castellari.IVaPS.Model
{
    /// <summary>
    /// rappresenta la checklist di un aereomobile
    /// </summary>
    public class Checklist
    {
        public string AircraftIcaoCode { get; set; }
        public string Vr { get; set; }
        public string Va { get; set; }
        public string Vne { get; set; }
        public string Vf0 { get; set; }
        public string Vapp { get; set; }
        public string Vldg { get; set; }
        public string Vs { get; set; }
        public List<ChecklistPhase> Phases { get; set; }
    }
}
