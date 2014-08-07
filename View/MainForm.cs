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

using Castellari.IVaPS.Control;
using Castellari.IVaPS.BLogic;
using System.Threading;

namespace Castellari.IVaPS.View
{
    public partial class MainForm : Form
    {
        private const int MILLISECOND_DOUBLE_CLICK_INTERVAL = 350;

        private IPSController controller;
        private DateTime lastBackSpeakingPressed = DateTime.MinValue;
        private DateTime lastPauseSpeakingPressed = DateTime.MinValue;
        private bool thereIsAFirstPausePressed = false;
        private bool thereIsAFirstBackPressed = false;
        static readonly object obj = new object();

        public MainForm()
        {
            InitializeComponent();
            notifyIcon.Visible = false;
        }

        public IPSController Controller
        {
            get
            {
                return controller;
            }
            set 
            {
                this.controller = value;
                mainPanel.Controller = this.controller; //per issue 25
            }
        }

        private void mainPanel_Load(object sender, EventArgs e)
        {

        }

        private void mainPanel_Resize(object sender, EventArgs e)
        {
            //per issue 21
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                //mostra in tray
                notifyIcon.Visible = true;
            }
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //per issue 21
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //issue 
            if (MessageBox.Show("Are you sure you want to exit?", "Confirm exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                controller.StopSpeaking();
                e.Cancel = true;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            AboutForm af = new AboutForm();
            af.Show();
        }


        //per gestione issue 25:


        private void hk_Foreground_Pressed(object sender, EventArgs e)
        {
            if (!notifyIcon.Visible)
            {
                Hide();
                notifyIcon.Visible = true;
            }
            else
            {
                notifyIcon_MouseDoubleClick(null, null);
            }
        }

        private void hk_position_Pressed(object sender, EventArgs e)
        {
            controller.SpeekCurrentPosition();
        }

        private void hk_speeds_Pressed(object sender, EventArgs e)
        {
            controller.SpeekChecklistSpeeds();
        }

        private void hk_checklist_Pressed(object sender, EventArgs e)
        {
            controller.ShowHideChecklistSelection();
        }

        private void hk_nextChecklist_Pressed(object sender, EventArgs e)
        {
            //mappato su CTRL+6
            //se la pressione è singola, mi comporto regolarmente, in due ravvicinate leggo la lista successiva
            DateTime now = DateTime.Now;
            long deltaT = now.Ticks - lastBackSpeakingPressed.Ticks;
            if (thereIsAFirstBackPressed && (deltaT) < (10000*MILLISECOND_DOUBLE_CLICK_INTERVAL))
            {
                //e la seconda pressione e si tratta di un doppio click
                lock (obj)
                {
                    controller.PreviousChecklistSelection();
                    thereIsAFirstBackPressed = false;
                    lastBackSpeakingPressed = DateTime.MinValue;
                }
            }
            else
            {
                //è la prima pressione o una seconda pressione fuori dall'intervallo "di sensibilità"
                thereIsAFirstBackPressed = true;
                lastBackSpeakingPressed = DateTime.Now;
                //così metto in pausa "a latere" per attendere il tempo previsto, per issue 106
                Thread temp = new Thread(delegate()
                    {
                        Thread.Sleep(MILLISECOND_DOUBLE_CLICK_INTERVAL);
                        lock (obj)
                        {
                            if (thereIsAFirstBackPressed)
                            {
                                //vuol dire che nessuno nel frattempo ha fatto un secondo click
                                thereIsAFirstBackPressed = false;
                                controller.RepeatCurrentChecklistSelection();
                            }
                        }
                    });
                temp.Start();   
            }
        }

        private void hk_map_Pressed(object sender, EventArgs e)
        {
            controller.ShowHideMaps();
        }

        private void hk_pauseresumespeak_Pressed(object sender, EventArgs e)
        {
            //mappato su CTRL+3
            //se la pressione è singola, mi comporto regolarmente, in due ravvicinate leggo la lista successiva
            DateTime now = DateTime.Now;
            long deltaT = now.Ticks - lastPauseSpeakingPressed.Ticks;
            if (thereIsAFirstPausePressed && (deltaT) < (10000*MILLISECOND_DOUBLE_CLICK_INTERVAL))
            {
                //e la seconda pressione e si tratta di un doppio click
                lock (obj)
                {
                    controller.NextChecklistSelection();
                    thereIsAFirstPausePressed = false;
                    lastPauseSpeakingPressed = DateTime.MinValue;
                }
            }
            else
            {
                //è la prima pressione o una seconda pressione fuori dall'intervallo "di sensibilità"
                thereIsAFirstPausePressed = true;
                lastPauseSpeakingPressed = DateTime.Now;
                //così metto in pausa "a latere" per attendere il tempo previsto, per issue 106
                Thread temp = new Thread(delegate()
                    {
                        Thread.Sleep(MILLISECOND_DOUBLE_CLICK_INTERVAL);
                        lock (obj)
                        {
                            if (thereIsAFirstPausePressed)
                            {
                                //vuol dire che nessuno nel frattempo ha fatto un secondo click
                                thereIsAFirstPausePressed = false;
                                controller.PauseResumeSpeaking();
                            }
                        }
                    });
                temp.Start();   
            }
        }

        private void utilbar_select_Pressed(object sender, EventArgs e)
        {
            controller.ShowHideUtilityBar();
        }

        private void utilbar_up_Pressed(object sender, EventArgs e)
        {
            controller.UtilityBarUp();
        }

        private void utilbar_down_Pressed(object sender, EventArgs e)
        {
            controller.UtilityBarDown();
        }
    }
}
