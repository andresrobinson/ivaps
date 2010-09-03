using System;
using System.Collections.Generic;
using System.Text;

namespace Castellari.IVaPS.Model
{
    /// <summary>
    /// Fase di una checklist, come pre-accensione, atterraggio, etc.
    /// </summary>
    public class ChecklistPhase
    {
        public string PhaseName { get; set; }
        public List<ChecklistItem> Items { get; set; }
    }
}
