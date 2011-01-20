namespace Castellari.IVaPS.View
{
    partial class UBMesageBox
    {
        /// <summary> 
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Liberare le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione componenti

        /// <summary> 
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare 
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbl_up = new System.Windows.Forms.Label();
            this.lbl_down = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbl_up
            // 
            this.lbl_up.AutoSize = true;
            this.lbl_up.ForeColor = System.Drawing.Color.LightGray;
            this.lbl_up.Location = new System.Drawing.Point(0, 0);
            this.lbl_up.Name = "lbl_up";
            this.lbl_up.Size = new System.Drawing.Size(10, 13);
            this.lbl_up.TabIndex = 0;
            this.lbl_up.Text = "-";
            // 
            // lbl_down
            // 
            this.lbl_down.AutoSize = true;
            this.lbl_down.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_down.ForeColor = System.Drawing.Color.LimeGreen;
            this.lbl_down.Location = new System.Drawing.Point(0, 11);
            this.lbl_down.Name = "lbl_down";
            this.lbl_down.Size = new System.Drawing.Size(11, 15);
            this.lbl_down.TabIndex = 1;
            this.lbl_down.Text = "-";
            // 
            // UBMesageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lbl_down);
            this.Controls.Add(this.lbl_up);
            this.Name = "UBMesageBox";
            this.Size = new System.Drawing.Size(300, 28);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label lbl_up;
        internal System.Windows.Forms.Label lbl_down;

    }
}
