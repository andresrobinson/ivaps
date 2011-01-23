//=========================================================
// This software is distributed under GPL v2 Licence
//
// Developed by Federico Castellari (fede.caste@gmail.com)
// January 2011
//
// Developed using Microsoft Visual C# 2008 Express Edition
//=========================================================

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
                int absoluteVorDirection = 360 - (int)status.CurrentPosition.Nav1Radial;
                directionIndicator1.DirectionAngle = (absoluteVorDirection - status.CurrentPosition.Heading);
            }
            else
            {
                lbl_dma.Text = "n/a";
            }
            Invalidate();
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
