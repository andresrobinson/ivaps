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
    public partial class UBMesageBox : UserControl, IUtilityBarItem
    {
        private static Color COLOR_HIGHLIGHTED = Color.LightGray;

        public UBMesageBox()
        {
            InitializeComponent();
        }

        public void UpdateView(FlightStatus status)
        {
            //noop
        }

        public void ShowMessage(string message)
        {
            BeginInvoke(new MyDelegate(this.InternalShowMessage), new object[] { message });
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

        private delegate void MyDelegate(string msg);

        private void InternalShowMessage(string msg)
        {
            lbl_up.Text = lbl_down.Text;
            lbl_down.Text = msg;
        }
    }
}
