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
    }
}
