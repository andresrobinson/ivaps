namespace Castellari.IVaPS.View
{
    partial class ConfigForm
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

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chk_fp = new System.Windows.Forms.CheckBox();
            this.chk_aot = new System.Windows.Forms.CheckBox();
            this.btn_save = new System.Windows.Forms.Button();
            this.txt_vaid = new System.Windows.Forms.TextBox();
            this.txt_callsign = new System.Windows.Forms.TextBox();
            this.ckb_trasponder = new System.Windows.Forms.CheckBox();
            this.cbo_chk = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Callsign:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Virtual Airline IVAO ID:";
            // 
            // chk_fp
            // 
            this.chk_fp.AutoSize = true;
            this.chk_fp.Location = new System.Drawing.Point(13, 67);
            this.chk_fp.Name = "chk_fp";
            this.chk_fp.Size = new System.Drawing.Size(164, 17);
            this.chk_fp.TabIndex = 3;
            this.chk_fp.Text = "Autoload FlightPlan at startup";
            this.chk_fp.UseVisualStyleBackColor = true;
            // 
            // chk_aot
            // 
            this.chk_aot.AutoSize = true;
            this.chk_aot.Location = new System.Drawing.Point(13, 91);
            this.chk_aot.Name = "chk_aot";
            this.chk_aot.Size = new System.Drawing.Size(143, 17);
            this.chk_aot.TabIndex = 4;
            this.chk_aot.Text = "Always on Top at startup";
            this.chk_aot.UseVisualStyleBackColor = true;
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(118, 162);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(75, 23);
            this.btn_save.TabIndex = 5;
            this.btn_save.Text = "Save";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // txt_vaid
            // 
            this.txt_vaid.Location = new System.Drawing.Point(131, 37);
            this.txt_vaid.MaxLength = 4;
            this.txt_vaid.Name = "txt_vaid";
            this.txt_vaid.Size = new System.Drawing.Size(46, 20);
            this.txt_vaid.TabIndex = 2;
            this.txt_vaid.Text = "6290";
            // 
            // txt_callsign
            // 
            this.txt_callsign.Location = new System.Drawing.Point(118, 10);
            this.txt_callsign.MaxLength = 7;
            this.txt_callsign.Name = "txt_callsign";
            this.txt_callsign.Size = new System.Drawing.Size(59, 20);
            this.txt_callsign.TabIndex = 1;
            this.txt_callsign.Text = "OVT0505";
            // 
            // ckb_trasponder
            // 
            this.ckb_trasponder.AutoSize = true;
            this.ckb_trasponder.Location = new System.Drawing.Point(13, 114);
            this.ckb_trasponder.Name = "ckb_trasponder";
            this.ckb_trasponder.Size = new System.Drawing.Size(152, 17);
            this.ckb_trasponder.TabIndex = 6;
            this.ckb_trasponder.Text = "Automatic Trasponder S/C";
            this.ckb_trasponder.UseVisualStyleBackColor = true;
            // 
            // cbo_chk
            // 
            this.cbo_chk.FormattingEnabled = true;
            this.cbo_chk.Location = new System.Drawing.Point(78, 135);
            this.cbo_chk.Name = "cbo_chk";
            this.cbo_chk.Size = new System.Drawing.Size(99, 21);
            this.cbo_chk.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 138);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Checklist:";
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(192, 187);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbo_chk);
            this.Controls.Add(this.ckb_trasponder);
            this.Controls.Add(this.txt_callsign);
            this.Controls.Add(this.txt_vaid);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.chk_aot);
            this.Controls.Add(this.chk_fp);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ConfigForm";
            this.Text = "IVaPS Config";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chk_fp;
        private System.Windows.Forms.CheckBox chk_aot;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.TextBox txt_vaid;
        private System.Windows.Forms.TextBox txt_callsign;
        private System.Windows.Forms.CheckBox ckb_trasponder;
        private System.Windows.Forms.ComboBox cbo_chk;
        private System.Windows.Forms.Label label3;
    }
}