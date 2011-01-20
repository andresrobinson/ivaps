using System;
using System.Collections.Generic;
using System.Text;

namespace Castellari.IVaPS.View
{
    /// <summary>
    /// Tipologia selezionabile di IUtilityBarItem
    /// </summary>
    public interface IUtilityBarItemSelectable : IUtilityBarItem
    {
        /// <summary>
        /// Indica all'item di "selezionarsi" o "deselezionarsi"
        /// </summary>
        bool UBSelected
        {
            get;
            set;
        }

        /// <summary>
        /// Template method invocato ogni volta che, col controllo selezionato, viene dato un input di incrase
        /// </summary>
        void UBPressedIncrase();

        /// <summary>
        /// Template method invocato ogni volta che, col controllo selezionato, viene dato un input di decrase
        /// </summary>
        void UBPressetDecrase();

    }
}
