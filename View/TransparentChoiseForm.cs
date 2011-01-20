//=========================================================
// This software is distributed under GPL v2 Licence
//
// Developed by Federico Castellari (fede.caste@gmail.com)
// November 2009
//
// Developed using Microsoft Visual C# 2008 Express Edition
//=========================================================

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Castellari.IVaPS.Control;

namespace Castellari.IVaPS.View
{

    /// <summary>
    /// Form di rappresentazione di una scelta tra n voci.
    /// L'uso standard è del tipo:
    /// 
    /// form.ChooseTitle = "Scegli tra:";
    /// form.AvailableChooses = new string[] { "zero", "uno", "due", "tre", "quattro", "cinque", "sei"};
    /// form.SelectedEvent += new TransparentChoiseForm.SelectedIndexHandler(this.HandleSelection);
    /// </summary>
    public partial class TransparentChoiseForm : Form
    {
        private static int CHOOSE_PER_PAGE = 9;

        private int currentPage = 0;
        private int totPages = 0;
        private string[] chooses = null;

        public TransparentChoiseForm()
        {
            InitializeComponent();
        }

        public  string ChooseTitle 
        {
            get {
                return lbl_title.Text;
            }
            set {
                lbl_title.Text = value;
                RefreshContent();
            } 
        }

        public string[] AvailableChooses 
        {
            get {
                return chooses;
            }
            set {
                chooses = value;
                totPages = (chooses.Length / CHOOSE_PER_PAGE) + 1;
                if ((chooses.Length % CHOOSE_PER_PAGE) == 0)
                    totPages--;

                if (totPages > 1) 
                    lbl_next.Text = "Press '0' to next page";
                else
                    lbl_next.Text = "";

                RefreshContent();
            }
        }

        public event SelectedIndexHandler SelectedEvent;

        public delegate void SelectedIndexHandler(int selectedIndex);

        private void RefreshContent()
        {
            if (chooses == null) return;

            txt_msg.Text = "";
            int partialCounter=0;
            for (int i = currentPage * CHOOSE_PER_PAGE; i < (currentPage + 1) * CHOOSE_PER_PAGE && (i < chooses.Length); i++)
            {
                partialCounter++;
                txt_msg.Text += partialCounter + ") " + chooses[i] + "\r\n\r\n";
            }
            txt_msg.SelectedText = "";
        }

        private void txt_msg_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == '0')
                {
                    //gestione del cambio pagina
                    SwitchPage();
                }
                else if (e.KeyChar == 27)
                {
                    //gestione dell'ESC
                    this.Visible = false;
                }
                else
                {
                    int selectedIndex = 0;
                    if (!int.TryParse(e.KeyChar.ToString(), out selectedIndex)) return;
                    int selectedValue = currentPage * CHOOSE_PER_PAGE + selectedIndex - 1;
                    if (selectedValue >= chooses.Length) return;
                    SelectedEvent(selectedValue);
                }
            }
            catch (Exception)
            {
                //noop
            }
        }

        private void SwitchPage()
        {
            if (totPages == currentPage+1)
            {
                //è l'ultima pagina
                currentPage = 0;
            }
            else
            {
                currentPage++;
            }
            RefreshContent();
        }

        private void TransparentChoiseForm_Activated(object sender, EventArgs e)
        {
            txt_msg.DeselectAll();
        }
    }
}
