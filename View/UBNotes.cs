using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Castellari.IVaPS.View
{
    public partial class UBNotes : UserControl, IUtilityBarItemSelectable
    {
        private static Color COLOR_HIGHLIGHTED = Color.LightGray;
        private static Color COLOR_SELECTED = Color.Yellow;
        
        private NotesForm notesForm = new NotesForm();

        public UBNotes()
        {
            InitializeComponent();
            notesForm.Visible = false;
        }

        #region IUtilityBarItemSelectable Membri di

        public bool UBSelected
        {
            get
            {
                return BackColor == COLOR_SELECTED;
            }
            set
            {
                if (value)
                {
                    BackColor = COLOR_SELECTED;
                }
                else
                {
                    BackColor = Color.Transparent;
                }
            }
        }

        /// <summary>
        /// Mostra nascunde la form delle note
        /// </summary>
        public void UBPressedIncrase()
        {
            if (!notesForm.Visible)
            {
                notesForm.Location = new Point(this.Location.X - notesForm.Width / 2, this.Location.Y + this.Height + 2);
                notesForm.Visible = true;
            }
            else
                notesForm.Visible = false;
        }

        /// <summary>
        /// identico a Incrase
        /// </summary>
        public void UBPressetDecrase()
        {
            UBPressedIncrase();
        }

        #endregion

        #region IUtilityBarItem Membri di

        public bool UBHighlighted
        {
            get
            {
                return BackColor == COLOR_HIGHLIGHTED;
            }
            set
            {
                if (value)
                {
                    BackColor = COLOR_HIGHLIGHTED;
                }
                else
                {
                    BackColor = Color.Transparent;
                }
            }
        }

        public void UpdateView(Castellari.IVaPS.Model.FlightStatus status)
        {
            //noop
        }

        #endregion
    }
}
