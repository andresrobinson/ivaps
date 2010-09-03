using System;
using System.Collections.Generic;
using System.Text;

namespace Castellari.IVaPS.Model
{
    /// <summary>
    /// Coppia descrizione valore per un item di checklist
    /// </summary>
    public class ChecklistItem
    {
        public ChecklistItem()
        {
            Delay = 1;
        }

        /// <summary>
        /// descrizione della voce
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Valore della voce
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Ritardo in secondi da tenere dopo l'esposizione della coppia
        /// </summary>
        public int Delay { get; set; }
    }
}
