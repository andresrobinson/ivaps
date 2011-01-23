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
    public partial class HistogramIndicator : UserControl
    {
        private int percentage = 0;

        public HistogramIndicator()
        {
            InitializeComponent();
            Paint += new PaintEventHandler(HistogramIndicator_Paint);
            HistogramColor = Brushes.Black;
        }

        /// <summary>
        /// Colore con cui rappresentare la barra dell'istogramma
        /// </summary>
        public Brush HistogramColor { get; set; }

        /// <summary>
        /// Valore da 0 a 100% da rappresentare
        /// </summary>
        public int Percentage
        {
            get
            {
                return percentage;
            }
            set
            {
                this.percentage = value;
                Invalidate();//per forzare il repaint
            }
        }

        private void HistogramIndicator_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(HistogramColor, 0, Height - Height * Percentage / 100f, Width, Height * Percentage / 100f);
        }
    }
}
