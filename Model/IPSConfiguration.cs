using System;
using System.Collections.Generic;
using System.Text;

namespace Castellari.IVaPS.Model
{
    /// <summary>
    /// Classe contenente le configurazioni applicative
    /// </summary>
    public class IPSConfiguration
    {
        /// <summary>
        /// Tempo, in millisecondi, ogni quanto viene fatto polling verso le FSUIPC
        /// </summary>
        public const double TIMER_ELAPSED_MILLISECONDS = 1000;
        /// <summary>
        /// Altitudine in piedi oltre la quale le altitudini sono da considerare livelli
        /// </summary>
        public const double TRANSITION_ALTITUDE_FEET = 7000;
    }
}
