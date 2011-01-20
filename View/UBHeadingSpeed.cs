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
    public partial class UBHeadingSpeed : UserControl, IUtilityBarItem
    {
        private static Color COLOR_HIGHLIGHTED = Color.LightGray;

        public UBHeadingSpeed()
        {
            InitializeComponent();
        }

        public void UpdateView(FlightStatus status)
        {
            if (status.CurrentPosition != null)
            {
                lbl_hdg.Text = status.CurrentPosition.Heading.ToString("000") + "°";
                lbl_speed.Text = status.CurrentPosition.IndicatedAirspeedSpeed.ToString("000");
            }
            else
            {
                lbl_hdg.Text = "---°";
                lbl_speed.Text = "-";
            }
        }


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
    }
}

