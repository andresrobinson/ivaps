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

namespace Castellari.IVaPS.View
{
    public partial class MainForm : Form
    {
        private IPSController controller;

        public MainForm()
        {
            InitializeComponent();
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
                mainPanel.Controller = this.controller;
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
    }
}
