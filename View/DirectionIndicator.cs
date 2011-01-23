//=========================================================
// This software is distributed under GPL v2 Licence
//
// Developed by Federico Castellari (fede.caste@gmail.com)
// January 2011
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

namespace Castellari.IVaPS.View
{
    public partial class DirectionIndicator : UserControl
    {
        public double DirectionAngle { get; set; }

        public DirectionIndicator()
        {
            InitializeComponent();
            DirectionAngle = int.MinValue;
            this.Paint += new PaintEventHandler(DirectionIndicator_Paint);
        }

        void DirectionIndicator_Paint(object sender, PaintEventArgs e)
        {
            if (DirectionAngle != int.MinValue)
            {
                float halfWidht = (this.Width - 2) / 2;
                float halfHeigth = (this.Height - 2) / 2;
                double internalAngle = 90 - DirectionAngle;
                float radians = (float)internalAngle / 180f * (float)Math.PI;
                e.Graphics.DrawLine(Pens.Blue, halfWidht, halfHeigth, halfWidht + halfWidht*(float)Math.Cos(radians), halfHeigth - halfHeigth*(float)Math.Sin(radians));
            }    
        }
    }
}
