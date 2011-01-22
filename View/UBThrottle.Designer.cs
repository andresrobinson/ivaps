namespace Castellari.IVaPS.View
{
    partial class UBThrottle
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
            this.lbl_thtl = new System.Windows.Forms.Label();
            this.histogramIndicator1 = new Castellari.IVaPS.View.HistogramIndicator();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbl_thtl
            // 
            this.lbl_thtl.AutoSize = true;
            this.lbl_thtl.Location = new System.Drawing.Point(9, 17);
            this.lbl_thtl.Name = "lbl_thtl";
            this.lbl_thtl.Size = new System.Drawing.Size(10, 13);
            this.lbl_thtl.TabIndex = 0;
            this.lbl_thtl.Text = "-";
            // 
            // histogramIndicator1
            // 
            this.histogramIndicator1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.histogramIndicator1.Location = new System.Drawing.Point(0, 0);
            this.histogramIndicator1.Name = "histogramIndicator1";
            this.histogramIndicator1.Percentage = 0;
            this.histogramIndicator1.Size = new System.Drawing.Size(10, 30);
            this.histogramIndicator1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Thrtl";
            // 
            // UBThrottle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.histogramIndicator1);
            this.Controls.Add(this.lbl_thtl);
            this.Name = "UBThrottle";
            this.Size = new System.Drawing.Size(41, 30);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_thtl;
        private HistogramIndicator histogramIndicator1;
        private System.Windows.Forms.Label label1;
    }
}
