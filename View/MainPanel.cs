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
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Castellari.IVaPS.Control;
using Castellari.IVaPS.Model;
using System.Threading;

namespace Castellari.IVaPS.View
{
    public partial class MainPanel : UserControl
    {
        private const int MAP_AUTOUPDATE_DELAY_IN_SECONDS = 30;

        private FlightStatus model;
        private DateTime lastMapUpdate = DateTime.MinValue;
        private PirepForm pf = null;
        private MapForm mf;

        public IPSController Controller { get; set; }

        public MainPanel()
        {
            InitializeComponent();
        }

        private void btn_debug_Click(object sender, EventArgs e)
        {
            Controller.ShowHideLog();
        }

        private void btn_rec_Click(object sender, EventArgs e)
        {
            if (Controller.FetchFlightPlan(txt_callsign.Text,txt_va.Text))
            {
                Info("FP for " + model.Callsign + " loaded");
            }
            else
            {
                Error("IVAO FP for " + model.Callsign + " unavailable");
            }
        }

        private void btn_connect_Click(object sender, EventArgs e)
        {
            if (Controller.Connect())
            {
                if (Controller.StartStopRecording())
                {
                    Info("Successifully connected to FS!");
                    lbl_info.ForeColor = Color.LightGreen;
                }
                else
                    Error("Unable to start recording");
            }
            else
                Error("Unable to connect to FS");
        }

        private void btn_disconnect_Click(object sender, EventArgs e)
        {
            if (Controller.StartStopRecording())
                if (Controller.IsRecording)
                    Info("Rec started");
                else
                {
                    Info("Rec stopped");
                    if (Controller.Disconnect())
                        Info("Disconnected to FS");
                    else
                        Error("Unable to disconnect to FS");
                }
            else
                Error("Unable to connect to FS");
        }

        private void Info(string msg)
        {
            this.lbl_info.ForeColor = Color.DarkGreen;
            BeginInvoke(new MyDelegate(this.ShowMessage), new object[] {msg});
        }

        private void Error(string msg)
        {
            this.lbl_info.ForeColor = Color.Red;
            BeginInvoke(new MyDelegate(this.ShowMessage), new object[] { msg });
        }

        private delegate void MyDelegate(string msg);
        private delegate void DrawDelegate(FlightStatus stat);

        private void ShowMessage(string msg)
        {
            this.lbl_info.Text = msg;
        }

        public void DrawStatus(FlightStatus stat)
        {
            BeginInvoke(new DrawDelegate(this.Draw), new object[] { stat });
        }

        public void SetStatus(FlightStatus stat)
        {
            model = stat;
        }

        private void Draw(FlightStatus stat)
        {
            SetStatus(stat);
            if (stat.CurrentPosition != null)
            {
                lbl_lat.Text = stat.CurrentPosition.Latitude.ToString("00.000000") + "°";
                lbl_lon.Text = stat.CurrentPosition.Longitude.ToString("00.000000") + "°";
                lbl_speed.Text = stat.CurrentPosition.Speed.ToString("000.0") + " Knots";
                lbl_alt.Text = stat.CurrentPosition.Altitude.ToString("00000") + " ft";
                lbl_hdg.Text = stat.CurrentPosition.Heading.ToString("000") + "°";
            }
            lbl_dist.Text = stat.Distance.ToString("000") + " nm";
            lbl_maxHeight.Text = stat.MaxAltitude.ToString("000") + " ft";
            lbl_maxSpeed.Text = stat.MaxSpeed.ToString("000") + " kn";
            lbl_currFuel.Text = stat.CurrentFuel.ToString("0");
            lbl_fuelDep.Text = stat.DeparturenFuel.ToString("0");
            lbl_fuelArr.Text = stat.ArrivalFuel.ToString("0");

            if (stat.FlightPlan != null)
            {
                if (stat.ArrivalTime != DateTime.MinValue)
                {
                    lbl_arrTime.Text = stat.ArrivalTime.ToUniversalTime().ToShortTimeString();
                }
                if (stat.DepartureTime != DateTime.MinValue)
                {
                    lbl_depTime.Text = stat.DepartureTime.ToUniversalTime().ToShortTimeString();
                }
                if (stat.FlightPlan.Arrival != null)
                {
                    lbl_arrIcao.Text = stat.FlightPlan.Arrival.ICAOCode;
                }
                if (stat.FlightPlan.Departure != null)
                {
                    lbl_depIcao.Text = stat.FlightPlan.Departure.ICAOCode;
                }
                if (stat.FlightPlan.Route != null)
                {
                    lbl_route.Text = stat.FlightPlan.Route;
                }
            }

            if (mf != null && mf.Visible && model != null && (lastMapUpdate.AddSeconds(MAP_AUTOUPDATE_DELAY_IN_SECONDS).CompareTo(DateTime.Now)<0))
            {
                mf.GoToPosition(model.CurrentPosition);
                lastMapUpdate = DateTime.Now;
            }
        }

        private void btn_google_Click(object sender, EventArgs e)
        {
            if (model != null)
            {
                if (mf == null)
                {
                    mf = new MapForm(new Point(this.Parent.Location.X + this.Parent.Width, this.Parent.Location.Y));
                    mf.Visible = false;
                }

                if (!mf.Visible)
                {
                    mf.GoToPosition(model.CurrentPosition);
                    mf.Visible = true;
                }
                else
                {
                    mf.Visible = false;
                }
            }
            else
                Info("Current position invalid");
        }

        private void btn_pirep_Click(object sender, EventArgs e)
        {
            if (pf == null)
            {
                Point p = new Point(this.Parent.Location.X + this.Parent.Width, this.Parent.Location.Y);
                pf = new PirepForm(p);//così ho un lazy loading, se non si usa non uso ram
            }

            if (pf.Visible)
            {
                pf.Visible = false;
            }
            else
            {
                if (model != null)
                {
                    model.Callsign = txt_callsign.Text;
                    model.VirtualAirlineID = txt_va.Text;
                    pf.FillPirep(model);
                    pf.Visible = true;
                    this.Draw(model);
                }
            }
        }

        private void MainPanel_Load(object sender, EventArgs e)
        {
            //connessione automatica
            btn_connect_Click(null, null);
        }
    }
}