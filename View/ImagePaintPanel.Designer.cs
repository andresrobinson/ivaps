namespace Castellari.IVaPS.View
{
    partial class ImagePaintPanel
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ImagePaintPanel
            // 
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.MouseLeave += new System.EventHandler(this.ImagePaintPanel_MouseLeave);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ImagePaintPanel_MouseMove);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ImagePaintPanel_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ImagePaintPanel_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
