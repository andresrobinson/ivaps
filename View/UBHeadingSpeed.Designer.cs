namespace Castellari.IVaPS.View
{
    partial class UBHeadingSpeed
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
            this.lbl_hdg = new System.Windows.Forms.Label();
            this.lbl_speed = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbl_hdg
            // 
            this.lbl_hdg.AutoSize = true;
            this.lbl_hdg.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_hdg.ForeColor = System.Drawing.Color.MediumBlue;
            this.lbl_hdg.Location = new System.Drawing.Point(0, 0);
            this.lbl_hdg.Name = "lbl_hdg";
            this.lbl_hdg.Size = new System.Drawing.Size(35, 14);
            this.lbl_hdg.TabIndex = 0;
            this.lbl_hdg.Text = "---°";
            // 
            // lbl_speed
            // 
            this.lbl_speed.AutoSize = true;
            this.lbl_speed.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_speed.ForeColor = System.Drawing.Color.MediumBlue;
            this.lbl_speed.Location = new System.Drawing.Point(0, 17);
            this.lbl_speed.Name = "lbl_speed";
            this.lbl_speed.Size = new System.Drawing.Size(28, 14);
            this.lbl_speed.TabIndex = 1;
            this.lbl_speed.Text = "---";
            // 
            // UBHeadingSpeed
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbl_speed);
            this.Controls.Add(this.lbl_hdg);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.MaximumSize = new System.Drawing.Size(37, 30);
            this.MinimumSize = new System.Drawing.Size(37, 30);
            this.Name = "UBHeadingSpeed";
            this.Size = new System.Drawing.Size(37, 30);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_hdg;
        private System.Windows.Forms.Label lbl_speed;
    }
}
