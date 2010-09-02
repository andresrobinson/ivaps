using System;
using System.Collections.Generic;
using System.Text;

namespace Castellari.IVaPS.Model
{
    /// <summary>
    /// Descrive lo stato di ivap e di alcuni suoi attributi
    /// </summary>
    public class IvapStatus
    {
        private static IvapStatus singleton = new IvapStatus();

        protected IvapStatus() { }//per prevenire costruzioni altre

        public static IvapStatus Instance
        {
            get
            {
                return singleton;
            }
        }

        /// <summary>
        /// Se a true dice che IVAP è runnante in quel momento
        /// </summary>
        public bool IsRunning{ get; set; }

        /// <summary>
        /// Se a true dice che il trasponder di IVAP è in Siera, se a false è in Charlie
        /// </summary>
        public bool IvapTrasponderIsInStandby { get; set; }

        /// <summary>
        /// Versione di FS a cui si è connessi
        /// </summary>
        public FlightSimulatorVersion FSVersion { get; set; }
    }
}
