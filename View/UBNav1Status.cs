using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Castellari.IVaPS.View
{
    public partial class UBNav1Status : UserControl, IUtilityBarItem
    {
        private static Color COLOR_HIGHLIGHTED = Color.LightGray;

        public UBNav1Status()
        {
            InitializeComponent();
        }

        public void UpdateView(Castellari.IVaPS.Model.FlightStatus status)
        {
            if (status.CurrentPosition != null)
            {
                if(!double.IsNaN(status.CurrentPosition.Nav1DME)) lbl_dma.Text = status.CurrentPosition.Nav1DME.ToString("0.0");
                else lbl_dma.Text = "n/a"; 
                crossIndicator1.HorizontalError = (float)status.CurrentPosition.Nav1Localizer / 127f * 100f;
                crossIndicator1.VerticalError = (float)status.CurrentPosition.Nav1Glide / 127f * 100f;
                directionIndicator1.DirectionAngle = (((int)status.CurrentPosition.Nav1Radial + 180) - status.CurrentPosition.Heading);
            }
            else
            {
                lbl_dma.Text = "n/a";
            }
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
        #endregion
    }
}
