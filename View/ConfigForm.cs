using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Castellari.IVaPS.Model;
using Castellari.IVaPS.Control;
using Castellari.IVaPS.BLogic;

namespace Castellari.IVaPS.View
{
    public partial class ConfigForm : Form
    {
        IPSController controller = null;

        public ConfigForm(IPSController controller)
        {
            InitializeComponent();
            this.controller = controller;
            try
            {
                IPSConfiguration.Load();
                txt_callsign.Text = IPSConfiguration.CALLSIGN;
                txt_vaid.Text = IPSConfiguration.VA_ID;
                chk_aot.Checked = IPSConfiguration.AUTO_ALWAYSONTOP;
                chk_fp.Checked = IPSConfiguration.AUTOLOAD_FLIGHTPLAN;
                ckb_trasponder.Checked = IPSConfiguration.AUTO_TRASPONDER;
                string[] tmp = ChecklistReader.ReadAvailableChecklists();
                foreach (string s in tmp)
                {
                    cbo_chk.Items.Add(s);
                }
                if (IPSConfiguration.CURRENT_CHECKLIST != null)
                {
                    cbo_chk.SelectedItem = IPSConfiguration.CURRENT_CHECKLIST;
                }
                
            }
            catch 
            {
                txt_callsign.Text = "xxxxxxx";
                txt_vaid.Text = "xxxx";
                chk_aot.Checked = false;
                chk_fp.Checked = false;
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            IPSConfiguration.CALLSIGN = txt_callsign.Text;
            IPSConfiguration.VA_ID = txt_vaid.Text;
            IPSConfiguration.AUTO_ALWAYSONTOP = chk_aot.Checked;
            IPSConfiguration.AUTOLOAD_FLIGHTPLAN = chk_fp.Checked;
            IPSConfiguration.AUTO_TRASPONDER = ckb_trasponder.Checked;
            IPSConfiguration.CURRENT_CHECKLIST = cbo_chk.SelectedItem.ToString();
            controller.SaveConfig();
            this.Close();
        }
    }
}
