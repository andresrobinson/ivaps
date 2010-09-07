namespace Castellari.IVaPS.View
{
    partial class TransparentChoiseForm
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
            this.txt_msg = new System.Windows.Forms.TextBox();
            this.lbl_title = new System.Windows.Forms.Label();
            this.lbl_next = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txt_msg
            // 
            this.txt_msg.BackColor = System.Drawing.Color.DimGray;
            this.txt_msg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_msg.Cursor = System.Windows.Forms.Cursors.Default;
            this.txt_msg.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_msg.ForeColor = System.Drawing.Color.White;
            this.txt_msg.Location = new System.Drawing.Point(12, 32);
            this.txt_msg.Multiline = true;
            this.txt_msg.Name = "txt_msg";
            this.txt_msg.ReadOnly = true;
            this.txt_msg.Size = new System.Drawing.Size(268, 302);
            this.txt_msg.TabIndex = 0;
            this.txt_msg.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_msg_KeyPress);
            // 
            // lbl_title
            // 
            this.lbl_title.AutoSize = true;
            this.lbl_title.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_title.ForeColor = System.Drawing.Color.White;
            this.lbl_title.Location = new System.Drawing.Point(12, 13);
            this.lbl_title.Name = "lbl_title";
            this.lbl_title.Size = new System.Drawing.Size(49, 16);
            this.lbl_title.TabIndex = 1;
            this.lbl_title.Text = "lbl_title";
            // 
            // lbl_next
            // 
            this.lbl_next.AutoSize = true;
            this.lbl_next.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_next.ForeColor = System.Drawing.Color.White;
            this.lbl_next.Location = new System.Drawing.Point(175, 337);
            this.lbl_next.Name = "lbl_next";
            this.lbl_next.Size = new System.Drawing.Size(43, 13);
            this.lbl_next.TabIndex = 2;
            this.lbl_next.Text = "lbl_next";
            // 
            // TransparentChoiseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(292, 359);
            this.ControlBox = false;
            this.Controls.Add(this.lbl_next);
            this.Controls.Add(this.lbl_title);
            this.Controls.Add(this.txt_msg);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TransparentChoiseForm";
            this.Opacity = 0.75;
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Activated += new System.EventHandler(this.TransparentChoiseForm_Activated);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_msg;
        private System.Windows.Forms.Label lbl_title;
        private System.Windows.Forms.Label lbl_next;
    }
}