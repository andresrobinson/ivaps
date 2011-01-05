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
using Castellari.IVaPS.BLogic;

namespace Castellari.IVaPS.View
{
    public partial class MainPanel : UserControl
    {
        private FlightStatus model;

        public IPSController Controller { get; set; }

        public MainPanel()
        {
            InitializeComponent();
            black_panel.Paint += new PaintEventHandler(black_panel_Paint);
        }

        void black_panel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(Pens.DarkGreen, 0, 140, black_panel.Width, 140);
            e.Graphics.DrawLine(Pens.DarkGreen, 0, 80, black_panel.Width, 80);
            e.Graphics.DrawLine(Pens.DarkGreen, 105, 0, 105, 80);
        }

        private void btn_debug_Click(object sender, EventArgs e)
        {
            Controller.ShowHideLog();
        }

        private void btn_rec_Click(object sender, EventArgs e)
        {
            if (Controller.FetchFlightPlan())
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
            if (Controller == null)
                return;//fatto per il designer di VS2008

            //la prima condizione serve solo per non far incazzare il designer di Visual Studio!
            if (lbl_connect != null && lbl_connect.Text == "Connect to FS")
            {
                //sono disconnesso, quindi devo connettermi
                if (Controller.Connect())
                {
                    if (Controller.StartStopRecording())
                    {
                        Info("Successifully connected to FS!");
                        lbl_info.ForeColor = Color.LightGreen;
                        lbl_connect.Text = "Disconnect";
                    }
                    else
                        Error("Unable to start recording");
                }
                else
                    Error("Unable to connect to FS");
                
            }
            else
            {
                if (Controller.StartStopRecording())
                    if (Controller.IsRecording)
                        Info("Rec started");
                    else
                    {
                        Info("Rec stopped");
                        if (Controller.Disconnect())
                        {
                            Info("Disconnected to FS");
                            lbl_connect.Text = "Connect to FS";
                        }
                        else
                            Error("Unable to disconnect to FS");
                    }
                else
                    Error("Unable to connect to FS");
            }
        }

        private void btn_config_Click(object sender, EventArgs e)
        {
            ConfigForm cf = new ConfigForm(Controller);
            Point p = this.Parent.Location;
            p.Offset(this.Parent.Width,0);
            cf.Location = p;
            cf.Visible = true;
        }

        public void Info(string msg)
        {
            this.lbl_info.ForeColor = Color.DarkGreen;
            BeginInvoke(new MyDelegate(this.ShowMessage), new object[] {msg});
        }

        public void Error(string msg)
        {
            this.lbl_info.ForeColor = Color.Red;
            try
            {
                BeginInvoke(new MyDelegate(this.ShowMessage), new object[] { msg });
            }
            catch
            {
                //ci si arriva in caso di chiusura del form prima della scrittura, quindi noop
            }
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
                lbl_speed.Text = stat.CurrentPosition.TrueAirspeedSpeed.ToString("000.0") + " Knots";
                lbl_alt.Text = stat.CurrentPosition.Altitude.ToString("00000") + " ft";
                lbl_hdg.Text = stat.CurrentPosition.Heading.ToString("000") + "°";
            }
            lbl_dist.Text = stat.Distance.ToString("000") + " nm";
            lbl_maxHeight.Text = stat.MaxAltitude.ToString("000") + " ft";
            lbl_maxSpeed.Text = stat.MaxSpeed.ToString("000") + " kn";
            lbl_currFuel.Text = stat.CurrentFuel.ToString("0");
            lbl_fuelDep.Text = stat.DepartureFuel.ToString("0");
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
                    //issue 17
                    if (stat.FlightPlan.Route.Length > 23)
                    {
                        lbl_route.Text = stat.FlightPlan.Route.Substring(0,23) + "...";
                        lbl_route_tooltip.SetToolTip(lbl_route, stat.FlightPlan.Route);
                    }
                    else
                    {
                        lbl_route.Text = stat.FlightPlan.Route;
                        lbl_route_tooltip.SetToolTip(lbl_route, "");
                    }
                }
            }

            //issue 29
            switch (stat.CurrentStatus)
            {
                case FlightStates.Before_Departed:
                    lbl_status.Text = "Parked"; break;
                case FlightStates.Engine_Started:
                    lbl_status.Text = "Engine ON"; break;
                case FlightStates.TakeOffTaxi:
                    lbl_status.Text = "Taxi"; break;
                case FlightStates.Airborne:
                    lbl_status.Text = "Airborne"; break;
                case FlightStates.Landed:
                    lbl_status.Text = "Landed"; break;
                case FlightStates.OnBlocks:
                    lbl_status.Text = "On blocks"; break;
                case FlightStates.EngineOff:
                    lbl_status.Text = "Engine OFF"; break;
            }
        }

        private void btn_google_Click(object sender, EventArgs e)
        {
            MapForm mf = null;

            //debug per issue 60
            //mf = new MapForm(new Point(this.Parent.Location.X + this.Parent.Width, this.Parent.Location.Y), this.Controller);
            //mf.Visible = true;
            //AircraftPosition pos = new AircraftPosition();
            //pos.Latitude = 3.381824;
            //pos.Longitude = -76.464844;
            //mf.GoToPosition(pos);
            //fine debug

            if (model.CurrentPosition != null)
            {
                mf = new MapForm(new Point(this.Parent.Location.X + this.Parent.Width, this.Parent.Location.Y), this.Controller);
                mf.Visible = true;
                mf.GoToPosition(model.CurrentPosition);
            }
            else
                Error("Current position invalid");
        }

        private void btn_pirep_Click(object sender, EventArgs e)
        {
            if (model != null)
            {
                Point p = new Point(this.Parent.Location.X + this.Parent.Width, this.Parent.Location.Y);
                PirepForm pf = new PirepForm(p);
                model.Callsign = IPSConfiguration.CALLSIGN;
                model.VirtualAirlineID = IPSConfiguration.VA_ID;
                pf.FillPirep(model);
                pf.Visible = true;
            }
            else
            {
                Error("No data to fill pirep");
            }
        }

        private void MainPanel_Load(object sender, EventArgs e)
        {
            //connessione automatica
            btn_connect_Click(null, null);
        }

        public void btn_top_Click(object sender, EventArgs e)
        {
            MainForm fm = (MainForm)this.Parent;
            fm.TopMost = !fm.TopMost;
            btn_top.BackColor = fm.TopMost ? Color.Orange : Color.Transparent;
        }

        public void AsyncFPLoad()
        {
            Thread.Sleep(1000);
            btn_rec_Click(null, null);
        }

        private void btn_assist_Click(object sender, EventArgs e)
        {
            CrewForm af = new CrewForm();
            af.Show();
        }
    }
}