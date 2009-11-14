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
using System.Windows.Forms;

using Castellari.IVaPS.View;
using Castellari.IVaPS.Control;

namespace Castellari.IVaPS
{
    static class Program
    {
        /// <summary>
        /// Punto di ingresso principale dell'applicazione.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Costruzione dei moduli principali dell'applicazione
            MainForm view = new MainForm();
            IPSController controller = new IPSController(view);
            view.Controller = controller;
            //via
            Application.Run(view);
        }
    }
}
