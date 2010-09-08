using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using Castellari.IVaPS.BLogic;

namespace Castellari.IVaPS.View
{
    public partial class ImageViewer : Form
    {
        public static int MOVMENT_SIZE_VERTICAL = 20;
        public static int MOVMENT_SIZE_HORIZONTAL = 20;
        private string[] imagesPath = null;
        private int selectedImage = -1;

        public string[] ImagesPath 
        {
            get
            {
                return imagesPath;
            }
            set
            {
                imagesPath = value;
                SelectedImage = 0;
                lbl_avail.Text = "";
                for (int i = 0; i < imagesPath.Length; i++)
                {
                    lbl_avail.Text += (i+1) + ") " + ReadFileName(imagesPath[i]) + " ";
                }
                if (imagesPath.Length == 0)
                    lbl_msg.Text = "no images found in './" + ImageLoader.IMAGE_FOLDER_RELATIVE_PATH + "/' folder";
            }
        }

        public ImageViewer()
        {
            InitializeComponent();
        }

        private int SelectedImage
        {
            get
            {
                return selectedImage;
            }
            set
            {
                if (imagesPath != null && (value < imagesPath.Length))
                {
                    selectedImage = value;
                    imagePaintPanel1.ImagePath = imagesPath[selectedImage];
                    lbl_msg.Text = "Current: " + ReadFileName(imagePaintPanel1.ImagePath);
                    Invalidate();
                }
            }
        }

        private void ImageViewer_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            int i = -1;
            if (int.TryParse(c.ToString(), out i))
            {
                i -= 1;//voglio farlo 1-based e non 0-based
                if (i < imagesPath.Length)
                {
                    SelectedImage = i;
                }
            }
            else switch (c)
            {
                case 'w':
                    imagePaintPanel1.MoveUpDown(MOVMENT_SIZE_VERTICAL);
                    break;
                case 's':
                    imagePaintPanel1.MoveUpDown(-MOVMENT_SIZE_VERTICAL);
                    break;
                case 'a':
                    imagePaintPanel1.MoveRightLeft(MOVMENT_SIZE_HORIZONTAL);
                    break;
                case 'd':
                    imagePaintPanel1.MoveRightLeft(-MOVMENT_SIZE_HORIZONTAL);
                    break;
                default:
                    break;
            }
        }
        private string ReadFileName(string fullPath)
        {
            return fullPath.Substring(fullPath.LastIndexOf("\\") + 1, fullPath.Length - fullPath.LastIndexOf("\\") - 1);
        }

        private void ImageViewer_Resize(object sender, EventArgs e)
        {
            
        }
    }
}
