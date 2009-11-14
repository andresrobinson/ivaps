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
using System.Text;

using Castellari.IVaPS.Model;
using Castellari.IVaPS.View;
using Castellari.IVaPS.BLogic;
using System.Reflection;

namespace Castellari.IVaPS.Control
{
    /// <summary>
    /// Il main controller dell'applicazione, è da qui che si controllano View e Model come da pattern MVC,
    /// e da qui si attiva la business logic sincrona
    /// </summary>
    public class IPSController
    {
        private FlightStatus status = null;
        private MainForm viewMainForm = null;
        private LogForm logForm = new LogForm();
        private LogUtil log = new LogUtil();
        private FSWrapper flightSim = null;

        public IPSController(MainForm view)
        {
            Log("Init application...");
            viewMainForm = view;
            viewMainForm.Text = "IvaoPirepSender 1.0 beta";
            flightSim = new FSWrapper();
            flightSim.Controller = this;
            status = new FlightStatus();
            viewMainForm.mainPanel.SetStatus(status);
            Log("..Init successifully terminated");
        }

        public void ShowHideLog()
        {
            if (logForm == null)
                logForm = new LogForm();

            if (logForm.Visible)
                logForm.Visible = false;
            else
                logForm.Visible = true;
                logForm.Content = log.CurrentLog;
        }

        public void Log(string msg)
        {
            log.Log(msg);
            if (logForm.Visible)
            {
                logForm.Content = log.CurrentLog;
            }
        }

        public bool Connect()
        {
            try
            {
                if (!flightSim.IsConnected)
                {
                    flightSim.ConnectToFS();
                    return true;
                }
                else
                {
                    Log("FS is already connected");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log(ex.Message);
                return false;
            }
        }

        public bool Disconnect()
        {
            try
            {
                if (flightSim.IsConnected)
                {
                    flightSim.DisconnectToFS();
                    return true;
                }
                else
                {
                    Log("FS is already disconnected");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log(ex.Message);
                return false;
            }
        }

        public bool StartStopRecording()
        {
            try
            {
                if (flightSim.IsConnected)
                {
                    if (flightSim.IsRecording)
                    {
                        flightSim.StopRecording();
                        flightSim.FlightSimEvent -= new FSWrapper.FSEventHandler(this.HandleEvent);
                    }
                    else
                    {
                        flightSim.StartRecording();
                        flightSim.FlightSimEvent += new FSWrapper.FSEventHandler(this.HandleEvent);
                    }
                    return true;
                }
                else
                {
                    Log("FS not connected");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log(ex.Message);
                return false;
            }
        }

        public bool IsRecording
        {
            get
            {
                return flightSim.IsRecording;
            }
        }

        public bool FetchFlightPlan()
        {
            status.Callsign = viewMainForm.mainPanel.txt_callsign.Text;
            status.VirtualAirlineID = viewMainForm.mainPanel.txt_va.Text;

            IvaoFlightPlan fp = IPSUtils.RetrivePlan(status.Callsign);
            if (fp != null)
            {
                status.Arrival = fp.Arrival;
                status.Departure = fp.Departure;
                status.FlightPlan = fp.Route;
                status.FlightType = fp.FlightType;
                status.Alternate = fp.Alternate;
                status.Aircraft = fp.Aircraft;
                return true;
            }
            else
                return false;
        }

        private void HandleEvent(FSEvent e)
        {
            if (e is PositioningEvent)
            {
                PositioningEvent pe = (PositioningEvent)e;
                status.CurrentPosition = pe.Position;
                viewMainForm.mainPanel.DrawStatus(status);
            }
            else if (e is TakeOffEvent)
            {
                status.DepartureTime = e.Timestamp;
                status.DeparturenFuel = status.CurrentFuel;
                viewMainForm.mainPanel.DrawStatus(status);
            }
            else if (e is LandingEvent)
            {
                status.ArrivalTime = e.Timestamp;
                status.ArrivalFuel = status.CurrentFuel; 
                viewMainForm.mainPanel.DrawStatus(status);
            }
            else
                throw new InvalidOperationException("non implementato");
        }
        
    }
}
