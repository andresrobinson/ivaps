using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Castellari.IVaPS.Model;

namespace Castellari.IVaPS.View
{
    public partial class UtilityBar : Form
    {
        private IUtilityBarItem[] ubItems = null;
        private UBMesageBox mb = null;
        private int highlightedIndex = 0;
        private int selectedIndex = -1;

        private FlightStatus lastStatus = null;

        public UtilityBar()
        {
            InitializeComponent();
            this.Width = Screen.GetBounds(this).Width;
            this.Height = 30;
            mb = new UBMesageBox();
            ubItems = new IUtilityBarItem[]{
                new UBHeadingSpeed(),
                new UBAltitude(),
                new UBcrs(),
                new UBAPAlt(),
                new UBAPSpd(),
                new UBNav1(),
                new UBNav1Status(),
                new UBAPcrs(),
                new UBNav2(),
                new UBADF(),
                new UBThrottle(),
                new UBTimer(),
                mb
            };

            int currX = 0;
            for (int i = 0; i < ubItems.Length; i++)
            {
                UserControl uc = (UserControl)ubItems[i];
                uc.Location = new System.Drawing.Point(currX + 1, 0);
                uc.Height = 30;
                uc.TabIndex = i;
                //Aggiungo il controllo
                this.Controls.Add(uc);
                //ricalcolo l'avanzamento
                currX += uc.Width;
            }
            UpdateView(lastStatus);
        }

        private void Highlight()
        {
            for (int i=0; i< ubItems.Length; i++)
            {
                    if (ubItems[i] is IUtilityBarItemSelectable)
                    {
                        ((IUtilityBarItemSelectable)ubItems[i]).UBSelected = (i == selectedIndex);
                    }
                    if(i!= selectedIndex) ubItems[i].UBHighlighted = (i == highlightedIndex);
            }
        }


        /// <summary>
        /// Da invocare per aggiornare i dati visualizzati
        /// </summary>
        /// <param name="status"></param>
        public void UpdateView(FlightStatus status)
        {
            lock (this)
            {
                if(status != null) 
                    lastStatus = status;
                BeginInvoke(new DrawDelegate(this.Draw), new object[0]);
            }

        }

        public void PressedSelect()
        {
            if (!(ubItems[highlightedIndex] is IUtilityBarItemSelectable))
                this.Visible = false;
            else
            {
                IUtilityBarItemSelectable tmp = (IUtilityBarItemSelectable)ubItems[highlightedIndex];
                if (!tmp.UBSelected)
                {
                    selectedIndex = highlightedIndex;
                    tmp.UBSelected = true;
                }
                else
                {
                    tmp.UBSelected = false;
                    selectedIndex = -1;
                }
            }


            UpdateView(null);
        }

        public void PressedUp()
        {
            if (selectedIndex < 0)
            {
                //nulla di selezionato
                if (highlightedIndex < (ubItems.Length - 1))
                {
                    highlightedIndex++;
                }
                else
                    highlightedIndex = 0;
            }
            else
            {
                IUtilityBarItemSelectable tmp = (IUtilityBarItemSelectable)ubItems[selectedIndex];
                tmp.UBPressedIncrase();
            }

            UpdateView(null);
        }

        public void PressedDown()
        {
            if (selectedIndex < 0)
            {
                //nulla di selezionato
                if (highlightedIndex > 0)
                {
                    highlightedIndex--;
                }
                else
                    highlightedIndex = ubItems.Length - 1;
            }
            else
            {
                IUtilityBarItemSelectable tmp = (IUtilityBarItemSelectable)ubItems[selectedIndex];
                tmp.UBPressetDecrase();
            }

            UpdateView(null);
        }

        public void ShowMessage(string message)
        {
            mb.ShowMessage(message);
        }

        private delegate void DrawDelegate();

        private void Draw()
        {
            Highlight();

            if (lastStatus != null)
            {
                foreach (IUtilityBarItem item in ubItems)
                {
                    item.UpdateView(lastStatus);
                }
            }
        }

        #region medoti di grafica pura
        /// <summary>
        /// Questo override copia&incollato dalla rete rende impossibile dare il focus alla finestra, 
        /// proprio quello che mi serviva!
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            try
            {
                // Ignore all messages that try to set the focus.
                if (m.Msg != 0x7)
                {
                    base.WndProc(ref m);
                }
            }
            catch (Exception)
            {
                //noop inserito perchè a volte, durante il test, salta senza che capisca il perchè
            }
        }
        #endregion
    }
}

