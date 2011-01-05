using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Castellari.IVaPS.View
{
    public partial class ImagePaintPanel : Panel
    {
        private Point CurrentCorner = new Point(0, 0);
        private bool isDragging = false;
        private Point lastMousePosDuringDrag = Point.Empty;
        private string imagePath = null;
        private Image img = null;
        private Point lastClickCenter = Point.Empty;
        private Point lastClickEndpoint = Point.Empty;

        public ImagePaintPanel()
        {
            InitializeComponent();
        }

        public ImagePaintPanel(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public string ImagePath 
        {
            get
            {
                return imagePath;
            }
            set
            {
                imagePath = value;
                CurrentCorner = new Point(0, 0);
                if (ImagePath != null)
                    img = Image.FromFile(ImagePath);
                Invalidate();
            }
        }

        public void MoveUpDown(int size)
        {
            CurrentCorner.Y += size;
            Invalidate();
        }

        public void MoveRightLeft(int size)
        {
            CurrentCorner.X += size;
            Invalidate();
        }

        private void MoveExactly(int deltax, int deltay)
        {
            CurrentCorner.X += deltax;
            CurrentCorner.Y += deltay;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (img == null) return;
            e.Graphics.DrawImage(img , CurrentCorner);
            //issue 76
            if (lastClickCenter != Point.Empty)
            {
                e.Graphics.DrawLine(Pens.Red, lastClickCenter, lastClickEndpoint);
                e.Graphics.FillRectangle(Brushes.White, lastClickEndpoint.X + 15, lastClickEndpoint.Y + 5, 85, 30);
                double angleInRadians = Math.Atan2(lastClickEndpoint.X - lastClickCenter.X, lastClickCenter.Y - lastClickEndpoint.Y);
                if (angleInRadians < 0) angleInRadians += (2 * Math.PI);
                double angleInDegrees = (angleInRadians * 360) / (2 * Math.PI);
                e.Graphics.DrawString("QDR: " + angleInDegrees.ToString("000") + "°", new Font("Arial", 12, FontStyle.Bold), Brushes.Red, lastClickEndpoint.X + 15, lastClickEndpoint.Y + 5);
                double length = Math.Sqrt(Math.Pow(lastClickEndpoint.X - lastClickCenter.X,2) + Math.Pow(lastClickCenter.Y - lastClickEndpoint.Y,2));
                e.Graphics.DrawString(Convert.ToInt32(length) + " px", new Font("Arial", 8, FontStyle.Bold), Brushes.Red, lastClickEndpoint.X + 16, lastClickEndpoint.Y + 20);            
            }
        }

        private void ImagePaintPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                MoveExactly(e.X - lastMousePosDuringDrag.X, e.Y - lastMousePosDuringDrag.Y);
                lastMousePosDuringDrag = e.Location;
            }
            else if (lastClickCenter != Point.Empty)
            {
                //issue 76
                lastClickEndpoint = e.Location;
                this.Invalidate();
            }
        }

        private void ImagePaintPanel_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
            lastMousePosDuringDrag = Point.Empty;
        }

        private void ImagePaintPanel_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            lastMousePosDuringDrag = e.Location;
        }

        private void ImagePaintPanel_MouseLeave(object sender, EventArgs e)
        {
            ImagePaintPanel_MouseUp(null, null);
        }

        //per issue 76
        private void ImagePaintPanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (lastClickCenter == Point.Empty)
                {
                    lastClickCenter = e.Location;
                }
                else
                {
                    lastClickCenter = Point.Empty;
                }
                lastClickEndpoint = Point.Empty;
            }
        }
    }
}
