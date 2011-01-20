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
    public partial class UBAPSpd : UserControl, IUtilityBarItemSelectable
    {
        private static Color COLOR_HIGHLIGHTED = Color.LightGray;
        private static Color COLOR_SELECTED = Color.Yellow;

        public UBAPSpd()
        {
            InitializeComponent();
        }

        public void UpdateView(FlightStatus status)
        {
            //todo
        }


        #region IUtilityBarItemSelectable Membri di

        public bool UBSelected
        {
            get
            {
                return BackColor == COLOR_SELECTED;
            }
            set
            {
                if (value)
                {
                    BackColor = COLOR_SELECTED;
                }
                else
                {
                    BackColor = Color.Transparent;
                }
            }
        }

        public void UBPressedIncrase()
        {
            if (!UBSelected) return;
            //DA RIMUOVERE; questo è solo per test
            lbl_freq.Text = "up";
        }

        public void UBPressetDecrase()
        {
            if (!UBSelected) return;
            //DA RIMUOVERE; questo è solo per test
            lbl_freq.Text = "down";
        }

        #endregion

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
