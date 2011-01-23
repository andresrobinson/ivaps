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
    /// <summary>
    /// Elemento della utility bar col cronometro
    /// </summary>
    public partial class UBTimer : UserControl, IUtilityBarItemSelectable   
    {
        private static Color COLOR_HIGHLIGHTED = Color.LightGray;
        private static Color COLOR_SELECTED = Color.Yellow;

        private TimeSpan timerTime = new TimeSpan();
        private bool timerRunning = false;
        private DateTime lastCheck = DateTime.MinValue;

        public UBTimer()
        {
            InitializeComponent();
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

        /// <summary>
        /// START/STOP del timer
        /// </summary>
        public void UBPressedIncrase()
        {
            timerRunning = !timerRunning;
            if (timerRunning)
                lastCheck = DateTime.Now;
        }

        /// <summary>
        /// RESET del timer
        /// </summary>
        public void UBPressetDecrase()
        {
            timerTime = new TimeSpan();
            lastCheck = DateTime.MinValue;
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

        public void UpdateView(Castellari.IVaPS.Model.FlightStatus status)
        {
            if (lastCheck == DateTime.MinValue)
            {
                lbl_min.ForeColor = Color.Black;
                lbl_sec.ForeColor = Color.Black;
                lbl_min.Text = "--:";
                lbl_sec.Text = "--";
            }
            else
            {
                if (timerRunning)
                {
                    lbl_min.ForeColor = Color.Blue;
                    lbl_sec.ForeColor = Color.Blue;
                    timerTime += DateTime.Now - lastCheck;
                    lastCheck = DateTime.Now;
                }
                else
                {
                    lbl_min.ForeColor = Color.Black;
                    lbl_sec.ForeColor = Color.Black;
                }
                lbl_min.Text = timerTime.Minutes.ToString("00")+":";
                lbl_sec.Text = timerTime.Seconds.ToString("00");
            }
        }

        #endregion
    }
}
