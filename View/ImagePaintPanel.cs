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
        }

        private void ImagePaintPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                MoveExactly(e.X - lastMousePosDuringDrag.X, e.Y - lastMousePosDuringDrag.Y);
                lastMousePosDuringDrag = e.Location;
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
    }
}
