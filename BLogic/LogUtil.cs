﻿//=========================================================
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
    public class LogUtil
    {
        private StringBuilder logBuffer = new StringBuilder();

        public void Log(string msg)
        {
            logBuffer.AppendLine(msg);
            //logBuffer.Append(@"\r\n");
        }

        public string CurrentLog
        {
            get 
            {
                return logBuffer.ToString();
            }
        }
    }
}
