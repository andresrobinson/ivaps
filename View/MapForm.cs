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

namespace Castellari.IVaPS.View
{
    public partial class MapForm : Form
    {
        private Point windowspos = Point.Empty;

        public MapForm(Point initialPosition)
        {
            InitializeComponent();
            windowspos = initialPosition;
            this.Location = windowspos;
        }

        public void GoToPosition(AircraftPosition pos)
        {
            this.Location = windowspos;
            if (pos == null)
            {
                Text = "Position currently unavailable";
                this.Location = windowspos;
                return;
            }
            Text = "lat: " + pos.Latitude.ToString("00.00000") + " lon: " + pos.Longitude.ToString("00.00000");
            string tmp = string.Format("http://maps.google.it/maps?ie=UTF8&t=h&ll={0},{1}&z=13&output=embed",
                pos.Latitude.ToString("00.00000").Replace(',', '.'),
                pos.Longitude.ToString("00.00000").Replace(',', '.'));
            webBrowser1.Url = new Uri(tmp);
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            this.Location = windowspos;
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }
    }
}
