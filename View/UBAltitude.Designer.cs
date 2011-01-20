namespace Castellari.IVaPS.View
{
    partial class UBAltitude
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
            this.lbl_qnh = new System.Windows.Forms.Label();
            this.lbl_alt = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbl_qnh
            // 
            this.lbl_qnh.AutoSize = true;
            this.lbl_qnh.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_qnh.ForeColor = System.Drawing.Color.Black;
            this.lbl_qnh.Location = new System.Drawing.Point(4, 17);
            this.lbl_qnh.Name = "lbl_qnh";
            this.lbl_qnh.Size = new System.Drawing.Size(35, 14);
            this.lbl_qnh.TabIndex = 1;
            this.lbl_qnh.Text = "----";
            // 
            // lbl_alt
            // 
            this.lbl_alt.AutoSize = true;
            this.lbl_alt.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_alt.ForeColor = System.Drawing.Color.MediumBlue;
            this.lbl_alt.Location = new System.Drawing.Point(0, 0);
            this.lbl_alt.Name = "lbl_alt";
            this.lbl_alt.Size = new System.Drawing.Size(42, 14);
            this.lbl_alt.TabIndex = 0;
            this.lbl_alt.Text = "-----";
            // 
            // UBAltitude
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbl_qnh);
            this.Controls.Add(this.lbl_alt);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "UBAltitude";
            this.Size = new System.Drawing.Size(43, 30);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_qnh;
        private System.Windows.Forms.Label lbl_alt;
    }
}
