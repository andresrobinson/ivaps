using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Castellari.IVaPS.View
{
    public partial class UBThrottle : UserControl, IUtilityBarItem
    {
        private static Color COLOR_HIGHLIGHTED = Color.LightGray;

        public UBThrottle()
        {
            InitializeComponent();
        }

        #region IUtilityBarItem Membri di

        public bool UBHighlighted
        {
            get
            {
                return BackColor == COLOR_HIGHLIGHTED;
            }
            set
            {
                if (value)
                {
                    BackColor = COLOR_HIGHLIGHTED;
                }
                else
                {
                    BackColor = Color.Transparent;
                }
            }
        }

        public void UpdateView(Castellari.IVaPS.Model.FlightStatus status)
        {
            if (status.CurrentPosition != null)
            {
                lbl_thtl.Text = status.CurrentPosition.ThrottlePercentage + "%";
                histogramIndicator1.Percentage = status.CurrentPosition.ThrottlePercentage;
            }
        }

        #endregion
    }
}
