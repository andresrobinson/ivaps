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
    public partial class UBAltitude : UserControl, IUtilityBarItem
    {
        private static Color COLOR_HIGHLIGHTED = Color.LightGray;

        public UBAltitude()
        {
            InitializeComponent();
        }

        public void UpdateView(FlightStatus status)
        {
            if (status.CurrentPosition != null)
            {
                lbl_alt.Text = status.CurrentPosition.Altitude.ToString("00000");
                lbl_qnh.Text = status.CurrentPosition.QNH.ToString("0000");
            }
            else
            {
                lbl_alt.Text = "-----";
                lbl_qnh.Text = "----";
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
