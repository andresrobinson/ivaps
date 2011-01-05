namespace Castellari.IVaPS.View
{
    partial class ImageViewer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageViewer));
            this.lbl_msg = new System.Windows.Forms.Label();
            this.lbl_avail = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.imagePaintPanel1 = new Castellari.IVaPS.View.ImagePaintPanel(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_msg
            // 
            this.lbl_msg.AutoSize = true;
            this.lbl_msg.Location = new System.Drawing.Point(3, 1);
            this.lbl_msg.Name = "lbl_msg";
            this.lbl_msg.Size = new System.Drawing.Size(42, 13);
            this.lbl_msg.TabIndex = 1;
            this.lbl_msg.Text = "lbl_msg";
            // 
            // lbl_avail
            // 
            this.lbl_avail.AutoSize = true;
            this.lbl_avail.Location = new System.Drawing.Point(4, 0);
            this.lbl_avail.Name = "lbl_avail";
            this.lbl_avail.Size = new System.Drawing.Size(45, 13);
            this.lbl_avail.TabIndex = 2;
            this.lbl_avail.Text = "lbl_avail";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel1.Controls.Add(this.lbl_msg);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(798, 18);
            this.panel1.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel2.Controls.Add(this.lbl_avail);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 580);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(798, 18);
            this.panel2.TabIndex = 4;
            // 
            // imagePaintPanel1
            // 
            this.imagePaintPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imagePaintPanel1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.imagePaintPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imagePaintPanel1.ImagePath = null;
            this.imagePaintPanel1.Location = new System.Drawing.Point(0, 0);
            this.imagePaintPanel1.Name = "imagePaintPanel1";
            this.imagePaintPanel1.Size = new System.Drawing.Size(798, 598);
            this.imagePaintPanel1.TabIndex = 0;
            // 
            // ImageViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(798, 598);
            this.ControlBox = false;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.imagePaintPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ImageViewer";
            this.Opacity = 0.75;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ImageViewer_KeyPress);
            this.Resize += new System.EventHandler(this.ImageViewer_Resize);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ImagePaintPanel imagePaintPanel1;
        private System.Windows.Forms.Label lbl_msg;
        private System.Windows.Forms.Label lbl_avail;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}