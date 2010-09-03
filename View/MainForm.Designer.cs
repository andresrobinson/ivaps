namespace Castellari.IVaPS.View
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.label1 = new System.Windows.Forms.Label();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.mainPanel = new Castellari.IVaPS.View.MainPanel();
            this.hk_Foreground = new Castellari.IVaPS.HotKeys.SystemHotkey(this.components);
            this.hk_position = new Castellari.IVaPS.HotKeys.SystemHotkey(this.components);
            this.hk_1 = new Castellari.IVaPS.HotKeys.SystemHotkey(this.components);
            this.hk_2 = new Castellari.IVaPS.HotKeys.SystemHotkey(this.components);
            this.hk_3 = new Castellari.IVaPS.HotKeys.SystemHotkey(this.components);
            this.hk_4 = new Castellari.IVaPS.HotKeys.SystemHotkey(this.components);
            this.hk_5 = new Castellari.IVaPS.HotKeys.SystemHotkey(this.components);
            this.hk_6 = new Castellari.IVaPS.HotKeys.SystemHotkey(this.components);
            this.hk7 = new Castellari.IVaPS.HotKeys.SystemHotkey(this.components);
            this.hk_8 = new Castellari.IVaPS.HotKeys.SystemHotkey(this.components);
            this.hk_9 = new Castellari.IVaPS.HotKeys.SystemHotkey(this.components);
            this.hk_0 = new Castellari.IVaPS.HotKeys.SystemHotkey(this.components);
            this.hk_speeds = new Castellari.IVaPS.HotKeys.SystemHotkey(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkGray;
            this.label1.Location = new System.Drawing.Point(0, 314);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(187, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Developed by F. Castellari (OVT0505)";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon.BalloonTipText = "IVaPS - Double Click to enlarge";
            this.notifyIcon.BalloonTipTitle = "IVaPS- Double Click to enlarge";
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "IVaPS";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // mainPanel
            // 
            this.mainPanel.BackColor = System.Drawing.Color.White;
            this.mainPanel.Controller = null;
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(189, 313);
            this.mainPanel.TabIndex = 0;
            this.mainPanel.Load += new System.EventHandler(this.mainPanel_Load);
            this.mainPanel.Resize += new System.EventHandler(this.mainPanel_Resize);
            // 
            // hk_Foreground
            // 
            this.hk_Foreground.Shortcut = System.Windows.Forms.Shortcut.CtrlA;
            this.hk_Foreground.Pressed += new System.EventHandler(this.hk_Foreground_Pressed);
            // 
            // hk_position
            // 
            this.hk_position.Shortcut = System.Windows.Forms.Shortcut.CtrlY;
            this.hk_position.Pressed += new System.EventHandler(this.hk_position_Pressed);
            // 
            // hk_1
            // 
            this.hk_1.Shortcut = System.Windows.Forms.Shortcut.Ctrl1;
            this.hk_1.Pressed += new System.EventHandler(this.hk_1_Pressed);
            // 
            // hk_2
            // 
            this.hk_2.Shortcut = System.Windows.Forms.Shortcut.Ctrl2;
            this.hk_2.Pressed += new System.EventHandler(this.hk_2_Pressed);
            // 
            // hk_3
            // 
            this.hk_3.Shortcut = System.Windows.Forms.Shortcut.Ctrl3;
            this.hk_3.Pressed += new System.EventHandler(this.hk_3_Pressed);
            // 
            // hk_4
            // 
            this.hk_4.Shortcut = System.Windows.Forms.Shortcut.Ctrl4;
            this.hk_4.Pressed += new System.EventHandler(this.hk_4_Pressed);
            // 
            // hk_5
            // 
            this.hk_5.Shortcut = System.Windows.Forms.Shortcut.Ctrl5;
            this.hk_5.Pressed += new System.EventHandler(this.hk_5_Pressed);
            // 
            // hk_6
            // 
            this.hk_6.Shortcut = System.Windows.Forms.Shortcut.Ctrl6;
            this.hk_6.Pressed += new System.EventHandler(this.hk_6_Pressed);
            // 
            // hk7
            // 
            this.hk7.Shortcut = System.Windows.Forms.Shortcut.Ctrl7;
            this.hk7.Pressed += new System.EventHandler(this.hk7_Pressed);
            // 
            // hk_8
            // 
            this.hk_8.Shortcut = System.Windows.Forms.Shortcut.Ctrl8;
            this.hk_8.Pressed += new System.EventHandler(this.hk_8_Pressed);
            // 
            // hk_9
            // 
            this.hk_9.Shortcut = System.Windows.Forms.Shortcut.Ctrl9;
            this.hk_9.Pressed += new System.EventHandler(this.hk_9_Pressed);
            // 
            // hk_0
            // 
            this.hk_0.Shortcut = System.Windows.Forms.Shortcut.Ctrl0;
            this.hk_0.Pressed += new System.EventHandler(this.hk_0_Pressed);
            // 
            // hk_speeds
            // 
            this.hk_speeds.Shortcut = System.Windows.Forms.Shortcut.CtrlE;
            this.hk_speeds.Pressed += new System.EventHandler(this.hk_speeds_Pressed);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(189, 329);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mainPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public MainPanel mainPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private Castellari.IVaPS.HotKeys.SystemHotkey hk_Foreground;
        private Castellari.IVaPS.HotKeys.SystemHotkey hk_position;
        private Castellari.IVaPS.HotKeys.SystemHotkey hk_1;
        private Castellari.IVaPS.HotKeys.SystemHotkey hk_2;
        private Castellari.IVaPS.HotKeys.SystemHotkey hk_3;
        private Castellari.IVaPS.HotKeys.SystemHotkey hk_4;
        private Castellari.IVaPS.HotKeys.SystemHotkey hk_5;
        private Castellari.IVaPS.HotKeys.SystemHotkey hk_6;
        private Castellari.IVaPS.HotKeys.SystemHotkey hk7;
        private Castellari.IVaPS.HotKeys.SystemHotkey hk_8;
        private Castellari.IVaPS.HotKeys.SystemHotkey hk_9;
        private Castellari.IVaPS.HotKeys.SystemHotkey hk_0;
        private Castellari.IVaPS.HotKeys.SystemHotkey hk_speeds;

    }
}

