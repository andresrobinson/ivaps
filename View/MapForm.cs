//=========================================================
// This software is distributed under GPL v2 Licence
//
// Developed by Federico Castellari (fede.caste@gmail.com)
// November 2009
//
// Developed using Microsoft Visual C# 2008 Express Edition
//=========================================================

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Castellari.IVaPS.Model;
using Castellari.IVaPS.Control;

namespace Castellari.IVaPS.View
{
    public partial class MapForm : Form
    {
        private const int MAP_AUTOUPDATE_DELAY_IN_SECONDS = 30;

        private DateTime lastMapUpdate = DateTime.MinValue;
        private IPSController controller = null;


        public MapForm(Point initialPosition, IPSController controller)
        {
            InitializeComponent();
            this.controller = controller;
            controller.PositionUpdated += new IPSController.PositionEventHandler(this.HandleEvent);
            this.Location = initialPosition;
        }

        public void GoToPosition(AircraftPosition pos)
        {
            if (pos == null)
            {
                Text = "Position currently unavailable";
                return;
            }
            Text = "lat: " + pos.Latitude.ToString("00.00000") + " lon: " + pos.Longitude.ToString("00.00000");
            string tmp = string.Format("http://maps.google.it/maps?ie=UTF8&t=h&ll={0},{1}&z=13&output=embed",
                pos.Latitude.ToString("00.00000").Replace(',', '.'),
                pos.Longitude.ToString("00.00000").Replace(',', '.'));

            webBrowser1.Url = new Uri(tmp);
        }

        /// <summary>
        /// Gestore dei soli eventi di posizionamento per poter forzare il repaint
        /// </summary>
        /// <param name="e"></param>
        public void HandleEvent(AircraftPosition pos)
        {
            if (lastMapUpdate.AddSeconds(MAP_AUTOUPDATE_DELAY_IN_SECONDS).CompareTo(DateTime.Now) < 0)
            {
                GoToPosition(pos);
                lastMapUpdate = DateTime.Now;
            }
        }

        private void MapForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            controller.PositionUpdated -= new IPSController.PositionEventHandler(this.HandleEvent);
        }
    }
}
