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
        private const string IVAO_SITE_VA_PIREP_PAGE = "http://www.ivao.aero/vasystem/admin/va_pirep.asp?Id=";

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
                    //per la gestione dei livelli di volo (issue 23)
                    if (fs.MaxAltitude > IPSConfiguration.TRANSITION_ALTITUDE_FEET)
                    {
                        //è un livello di volo
                        int flightLevel = ((int)fs.MaxAltitude / 1000) * 10;//non divido banalmente per 100 per approssimare l'ultima cifra
                        webBrowser1.Document.All["Altitude"].SetAttribute("value", flightLevel.ToString("0"));
                    }
                    else
                    {
                        webBrowser1.Document.All["Altitude"].SetAttribute("value", fs.MaxAltitude.ToString("0"));
                    }
                    

                    webBrowser1.Document.All["TasCruise"].SetAttribute("value", fs.MaxSpeed.ToString("0"));
                    webBrowser1.Document.All["DepTime"].SetAttribute("value", fs.DepartureTime.ToUniversalTime().Hour.ToString("00"));
                    webBrowser1.Document.All["ActDepTime"].SetAttribute("value", fs.DepartureTime.ToUniversalTime().Minute.ToString("00"));
                    webBrowser1.Document.All["Land_Hour"].SetAttribute("value", fs.ArrivalTime.ToUniversalTime().Hour.ToString("00"));
                    webBrowser1.Document.All["Land_Minute"].SetAttribute("value", fs.ArrivalTime.ToUniversalTime().Minute.ToString("00"));
                    webBrowser1.Document.All["Route"].SetAttribute("value", fs.FlightPlan.Route);
                    webBrowser1.Document.All["Type"].SetAttribute("value", fs.FlightPlan.FlightType);
                    if (fs.FlightPlan != null && fs.FlightPlan.Departure != null)
                        webBrowser1.Document.All["DepAirport"].SetAttribute("value", fs.FlightPlan.Departure.ICAOCode);
                    if (fs.FlightPlan != null && fs.FlightPlan.Arrival != null)
                    {
                        webBrowser1.Document.All["DestAirport"].SetAttribute("value", fs.FlightPlan.Arrival.ICAOCode);
                        webBrowser1.Document.All["LandAirport"].SetAttribute("value", fs.FlightPlan.Arrival.ICAOCode);
                    }
                    if (fs.FlightPlan != null && fs.FlightPlan.Alternate != null)
                        webBrowser1.Document.All["AltAirport"].SetAttribute("value", fs.FlightPlan.Alternate.ICAOCode);
                    webBrowser1.Document.All["Aircraft"].SetAttribute("value", fs.FlightPlan.Aircraft);
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
            webBrowser1.Url = new Uri(IVAO_SITE_VA_PIREP_PAGE + fs.VirtualAirlineID);
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

        private void button1_Click(object sender, EventArgs e)
        {
            webBrowser1.GoBack();
        }
    }
}
