using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Castellari.IVaPS.Model;

namespace Castellari.IVaPS.View
{
    /// <summary>
    /// interfaccia di definizione degli Item di Utility Bar
    /// </summary>
    public interface IUtilityBarItem
    {
        /// <summary>
        /// Indica all'item di "evidenziarsi" o "de-evidenziarsi".
        /// </summary>
        bool UBHighlighted
        {
            get;
            set;
        }

        void UpdateView(FlightStatus status);
    }
}
