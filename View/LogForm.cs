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

namespace Castellari.IVaPS.View
{
    public partial class LogForm : Form
    {
        public LogForm()
        {
            InitializeComponent();
            this.textBox1.ReadOnly = true;
        }

        public string Content
        {
            get
            {
                return this.textBox1.Text;
            }
            set
            {
                BeginInvoke(new MyDelegate(this.ShowMessage), new object[] { value });
            }
        }

        private delegate void MyDelegate(string msg);

        private void ShowMessage(string msg)
        {
            this.textBox1.Text = msg;
            this.textBox1.SelectionStart = this.textBox1.Text.Length;
            this.textBox1.ScrollToCaret();
        }
    }
}
