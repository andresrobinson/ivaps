namespace Castellari.IVaPS.View
{
    partial class UBNav1Status
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
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_dma = new System.Windows.Forms.Label();
            this.crossIndicator1 = new Castellari.IVaPS.View.CrossIndicator();
            this.directionIndicator1 = new Castellari.IVaPS.View.DirectionIndicator();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 9);
            this.label1.TabIndex = 0;
            this.label1.Text = "DMA1";
            // 
            // lbl_dma
            // 
            this.lbl_dma.AutoSize = true;
            this.lbl_dma.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_dma.ForeColor = System.Drawing.Color.Navy;
            this.lbl_dma.Location = new System.Drawing.Point(0, 15);
            this.lbl_dma.Name = "lbl_dma";
            this.lbl_dma.Size = new System.Drawing.Size(27, 13);
            this.lbl_dma.TabIndex = 1;
            this.lbl_dma.Text = "---.-";
            // 
            // crossIndicator1
            // 
            this.crossIndicator1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crossIndicator1.HorizontalError = float.NaN;
            this.crossIndicator1.Location = new System.Drawing.Point(35, 1);
            this.crossIndicator1.Margin = new System.Windows.Forms.Padding(0);
            this.crossIndicator1.Name = "crossIndicator1";
            this.crossIndicator1.Size = new System.Drawing.Size(27, 27);
            this.crossIndicator1.TabIndex = 2;
            this.crossIndicator1.VerticalError = float.NaN;
            // 
            // directionIndicator1
            // 
            this.directionIndicator1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.directionIndicator1.DirectionAngle = -2147483648;
            this.directionIndicator1.Location = new System.Drawing.Point(64, 1);
            this.directionIndicator1.Margin = new System.Windows.Forms.Padding(0);
            this.directionIndicator1.Name = "directionIndicator1";
            this.directionIndicator1.Size = new System.Drawing.Size(27, 27);
            this.directionIndicator1.TabIndex = 3;
            // 
            // UBNav1Status
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.directionIndicator1);
            this.Controls.Add(this.crossIndicator1);
            this.Controls.Add(this.lbl_dma);
            this.Controls.Add(this.label1);
            this.Name = "UBNav1Status";
            this.Size = new System.Drawing.Size(93, 30);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_dma;
        private CrossIndicator crossIndicator1;
        private DirectionIndicator directionIndicator1;
    }
}
