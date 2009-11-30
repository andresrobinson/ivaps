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
        private AircraftPosition lastPos = null;


        public MapForm(Point initialPosition, IPSController controller)
        {
            InitializeComponent();
            this.controller = controller;
            controller.PositionUpdated += new IPSController.PositionEventHandler(this.HandleEvent);
            this.Location = initialPosition;
        }

        public void GoToPosition(AircraftPosition pos)
        {
            lastPos = pos;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<html> ");
            sb.AppendLine("  <head> ");
            sb.AppendLine("    <meta http-equiv=\"content-type\" content=\"text/html; charset=utf-8\"/> ");
            sb.AppendLine("    <title>Google Maps JavaScript API Example: Map Markers</title> ");
            sb.AppendLine("    <script src=\"http://maps.google.com/maps?file=api&amp;v=2&amp;key=ABQIAAAAzr2EBOXUKnm_jVnk0OJI7xSosDVG8KKPE1-m51RBrvYughuyMxQ-i1QfUnH94QxWIa6N4U6MouMmBA\"");
            sb.AppendLine("            type=\"text/javascript\"></script> ");
            sb.AppendLine("    <script type=\"text/javascript\"> ");
            sb.AppendLine("    ");
            sb.AppendLine("    function initialize() {");
            sb.AppendLine("      if (GBrowserIsCompatible()) {");
            sb.AppendLine("        var map = new GMap2(document.getElementById(\"map_canvas\"));");
            sb.AppendLine("        var latitude = " + pos.Latitude.ToString("00.00000").Replace(',', '.') + ";");
            sb.AppendLine("        var longitude = " + pos.Longitude.ToString("00.00000").Replace(',', '.') + ";");
            sb.AppendLine("        map.setCenter(new GLatLng(latitude, longitude), 13);");
            sb.AppendLine("        map.addControl(new GSmallMapControl());");
            sb.AppendLine("        map.addControl(new GMapTypeControl());");
            sb.AppendLine("        map.setMapType(G_SATELLITE_MAP);");
            sb.AppendLine("          var point = new GLatLng(latitude,longitude);");
            sb.AppendLine("          var marker = new GMarker(point);");
            sb.AppendLine("          map.addOverlay(marker);");
            sb.AppendLine("      }");
            sb.AppendLine("    }");
            sb.AppendLine(" ");
            sb.AppendLine("    </script> ");
            sb.AppendLine("  </head> ");
            sb.AppendLine("  <body onload=\"initialize()\" onunload=\"GUnload()\"> ");
            sb.AppendLine("    <div id=\"map_canvas\" style=\"width: " + webBrowser1.Width + "px; height: " + webBrowser1.Height + "px\"></div> ");
            sb.AppendLine("  </body> ");
            sb.AppendLine("</html> ");

            webBrowser1.DocumentText = sb.ToString();

            //using (System.IO.StreamWriter sw = System.IO.File.CreateText(@"C:\provadoppia.html"))
            //{
            //    sw.Write(sb.ToString());
            //    sw.Close();
            //}
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

        private void webBrowser1_SizeChanged(object sender, EventArgs e)
        {
            if (lastPos != null)
                GoToPosition(lastPos);
        }
    }
}
