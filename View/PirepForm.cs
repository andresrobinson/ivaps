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
    public partial class PirepForm : Form
    {
        private FlightStatus fs = null;
        private Point pos = Point.Empty;

        public PirepForm(Point initialPosition)
        {
            InitializeComponent();
            pos = initialPosition;
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            this.Location = pos;
            HtmlElement callsignField = webBrowser1.Document.All["Callsign"];
            if (callsignField == null)
                Text = "Wrong page: please logon and retry";
            else
            {
                webBrowser1.Document.All["Fuel_Type"].SetAttribute("value", "G"); ;
                if (fs != null)
                {
                    string shortCallsign = fs.Callsign;
                    if (shortCallsign.Length == 7) shortCallsign = shortCallsign.Substring(3);
                    callsignField.SetAttribute("value", shortCallsign);
                    webBrowser1.Document.All["Distance"].SetAttribute("value", fs.Distance.ToString("0"));
                    webBrowser1.Document.All["Altitude"].SetAttribute("value", fs.MaxAltitude.ToString("0"));
                    webBrowser1.Document.All["TasCruise"].SetAttribute("value", fs.MaxSpeed.ToString("0"));
                    webBrowser1.Document.All["DepTime"].SetAttribute("value", fs.DepartureTime.ToUniversalTime().Hour.ToString());
                    webBrowser1.Document.All["ActDepTime"].SetAttribute("value", fs.DepartureTime.ToUniversalTime().Minute.ToString());
                    webBrowser1.Document.All["Land_Hour"].SetAttribute("value", fs.ArrivalTime.ToUniversalTime().Hour.ToString());
                    webBrowser1.Document.All["Land_Minute"].SetAttribute("value", fs.ArrivalTime.ToUniversalTime().Minute.ToString());
                    webBrowser1.Document.All["Route"].SetAttribute("value", fs.FlightPlan);
                    webBrowser1.Document.All["Type"].SetAttribute("value", fs.FlightType);
                    if (fs.Departure != null) 
                        webBrowser1.Document.All["DepAirport"].SetAttribute("value", fs.Departure.ICAOCode);
                    if (fs.Arrival != null)
                    {
                        webBrowser1.Document.All["DestAirport"].SetAttribute("value", fs.Arrival.ICAOCode);
                        webBrowser1.Document.All["LandAirport"].SetAttribute("value", fs.Arrival.ICAOCode);
                    }
                    if (fs.Alternate != null)
                        webBrowser1.Document.All["AltAirport"].SetAttribute("value", fs.Alternate.ICAOCode);
                    webBrowser1.Document.All["Aircraft"].SetAttribute("value", fs.Aircraft);
                    webBrowser1.Document.All["Fuel_Qty"].SetAttribute("value", (fs.DeparturenFuel-fs.ArrivalFuel).ToString("0"));
                    BeginInvoke(new DrawDelegate(this.ChangeTitle), new object[] { "Verify data before sent!" });
                }
            }
        }

        public void FillPirep(FlightStatus fs)
        {
            this.fs = fs;
            //BeginInvoke(new DrawDelegate(this.ChangeTitle), new object[] { "loading..." });
            Text = "loading...";
            webBrowser1.Url = new Uri("http://www.ivao.aero/vasystem/admin/va_pirep.asp?Id=" + fs.VirtualAirlineID);
        }

        private void webBrowser1_LocationChanged(object sender, EventArgs e)
        {
            webBrowser1_DocumentCompleted(null, null);
        }

        public void ChangeTitle(string title)
        {
            Text = title;
        }

        private delegate void DrawDelegate(String title);

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }
    }
}
