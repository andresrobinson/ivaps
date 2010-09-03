﻿//=========================================================
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

namespace Castellari.IVaPS.View
{
    public partial class MainForm : Form
    {
        private IPSController controller;

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

        private void hk_1_Pressed(object sender, EventArgs e)
        {
            controller.SpeekChecklistPhase(0);
        }

        private void hk_2_Pressed(object sender, EventArgs e)
        {
            controller.SpeekChecklistPhase(1);
        }

        private void hk_3_Pressed(object sender, EventArgs e)
        {
            controller.SpeekChecklistPhase(2);
        }

        private void hk_4_Pressed(object sender, EventArgs e)
        {
            controller.SpeekChecklistPhase(3);
        }

        private void hk_5_Pressed(object sender, EventArgs e)
        {
            controller.SpeekChecklistPhase(4);
        }

        private void hk_6_Pressed(object sender, EventArgs e)
        {
            controller.SpeekChecklistPhase(5);
        }

        private void hk7_Pressed(object sender, EventArgs e)
        {
            controller.SpeekChecklistPhase(6);
        }

        private void hk_8_Pressed(object sender, EventArgs e)
        {
            controller.SpeekChecklistPhase(7);
        }

        private void hk_9_Pressed(object sender, EventArgs e)
        {
            controller.SpeekChecklistPhase(8);
        }

        private void hk_0_Pressed(object sender, EventArgs e)
        {
            controller.SpeekChecklistPhase(9);
        }

        private void hk_speeds_Pressed(object sender, EventArgs e)
        {
            controller.SpeekChecklistSpeeds();
        }
    }
}
