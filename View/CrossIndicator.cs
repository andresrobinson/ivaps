using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Castellari.IVaPS.View
{
    public partial class CrossIndicator : UserControl
    {
        private float horizontalError = float.NaN;
        private float verticalError = float.NaN;

        public CrossIndicator()
        {
            InitializeComponent();
            this.Paint += new PaintEventHandler(CrossIndicator_Paint);
        }

        void CrossIndicator_Paint(object sender, PaintEventArgs e)
        {
            float halfWidht = (this.Width-2) / 2;
            float halfHeigth = (this.Height-2) / 2;

            //croce di riferimento
            e.Graphics.DrawLine(Pens.WhiteSmoke, halfWidht, 0, halfWidht, this.Height);
            e.Graphics.DrawLine(Pens.WhiteSmoke, 0, halfHeigth, this.Width, halfHeigth);
            //assi di scostamento
            if (Math.Abs(HorizontalError) <= 100)
            {
                e.Graphics.DrawLine(Pens.Blue, halfWidht + halfWidht / 100f * horizontalError, 0, halfWidht + halfWidht / 100f * horizontalError, this.Height);
            }
            if (Math.Abs(VerticalError) <= 100)
            {
                e.Graphics.DrawLine(Pens.Blue, 0, halfHeigth + halfHeigth / 100f * verticalError, this.Width, halfHeigth + halfHeigth / 100f * verticalError);
            }
        }

        public float HorizontalError
        {
            get 
            {
                return horizontalError;
            }
            set 
            {
                if (value < -100) horizontalError = -100;
                else if (value > 100) horizontalError = 100;
                else horizontalError = value;
            }
        }

        public float VerticalError
        {
            get
            {
                return verticalError;
            }
            set
            {
                if (value < -100) verticalError = -100;
                else if (value > 100) verticalError = 100;
                else verticalError = value;
            }
        }
    }
}
