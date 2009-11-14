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
using System.Text;

namespace Castellari.IVaPS.BLogic
{
    /// <summary>
    /// Classe di utility che "tiene in pancia" il buffer dei log.
    /// Si è fatta la scelta di non usare un framework di logging ddicato per tenere tutto leggero e per 
    /// consentire di vedere i log anche in UI e non su file (richiedendo una cfg specifica)
    /// </summary>
    public class LogUtil
    {
        private StringBuilder logBuffer = new StringBuilder();

        /// <summary>
        /// Logga il messaggio (lo aggiunge al buffer di log)
        /// </summary>
        /// <param name="msg">il messaggio da loggare</param>
        public void Log(string msg)
        {
            logBuffer.AppendLine(msg);
        }

        /// <summary>
        /// Torna (get only) il valore attuale del buffer
        /// </summary>
        public string CurrentLog
        {
            get 
            {
                return logBuffer.ToString();
            }
        }
    }
}
