using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;
using System.Collections;

namespace Castellari.IVaPS.Model
{
    /// <summary>
    /// Classe contenente le configurazioni applicative
    /// </summary>
    public class IPSConfiguration
    {
        private const string CONFIG_FILE_NAME = "ivaps.cfg";

        private static string callsign = null;//callsign del pilota
        private static string vaId = null;//id IVAO della Virtual Airline
        private static bool autoLoadFlightPlan = true;//true se si desidera di default tentare di caricare da IVAN il piano di volo
        private static bool autoAlwaysOnTop = true;//true se si desidera di default avere la finestra sempre in primpo piano
        private static string ivaoFpUrl = null;
        private static bool autoTrasponder = true;//true se si vuole la gestione automatica del modo trasponder Sierra e Charlie, issue 63
        private static string currentChecklist = null;

        /// <summary>
        /// Tempo, in millisecondi, ogni quanto viene fatto polling verso le FSUIPC
        /// </summary>
        public const double TIMER_ELAPSED_MILLISECONDS = 1000;
        /// <summary>
        /// Altitudine in piedi oltre la quale le altitudini sono da considerare livelli
        /// </summary>
        public const double TRANSITION_ALTITUDE_FEET = 7000;

        /// <summary>
        /// Torna true se la configurazione è stata inserita e quindi salvata
        /// </summary>
        private static bool IsValid
        {
            get
            {
                return callsign != null;
            }
        }

        /// <summary>
        /// carica la configurazione persistita
        /// </summary>
        public static void Load()
        {
            FileInfo fi = new FileInfo(CfgFileFullPath);
            FileStream fs = null;

            try
            {
                if (!fi.Exists)
                    throw new FileNotFoundException("Config file not exists", CfgFileFullPath);
                else
                    fs = fi.OpenRead();

                StreamReader sr = new StreamReader(fs);
                string buff = null;
                Hashtable acc = new Hashtable();
                while ((buff = sr.ReadLine()) != null && !string.IsNullOrEmpty(buff))
                {
                    string[] tmp = buff.Split('=');
                    acc.Add(tmp[0], tmp[1]);
                }

                CALLSIGN = (string)acc["CALLSIGN"];
                VA_ID = (string)acc["VA_ID"];
                AUTOLOAD_FLIGHTPLAN = bool.Parse((string)acc["AUTOLOAD_FLIGHTPLAN"]);
                AUTO_ALWAYSONTOP = bool.Parse((string)acc["AUTO_ALWAYSONTOP"]);
                if (acc["AUTO_TRASPONDER"] != null)
                    AUTO_TRASPONDER = bool.Parse((string)acc["AUTO_TRASPONDER"]);
                else
                    AUTO_TRASPONDER = true;
                IVAO_FP_URL = (string)acc["IVAO_FP_URL"];
                CURRENT_CHECKLIST = (string)acc["CURRENT_CHECKLIST"];
                if (IVAO_FP_URL == null)
                    IVAO_FP_URL = "http://de3.www.ivao.aero/whazzup.txt";
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
        }

        /// <summary>
        /// Persiste la configurazione
        /// </summary>
        public static void Save()
        {
            FileInfo fi = new FileInfo(CfgFileFullPath);
            FileStream fs = null;

            try
            {
                if (!fi.Exists)
                    fs = fi.Create();
                else
                    fs = fi.OpenWrite();

                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine("CALLSIGN={0}", CALLSIGN);
                sw.WriteLine("VA_ID={0}", VA_ID);
                sw.WriteLine("AUTOLOAD_FLIGHTPLAN={0}", AUTOLOAD_FLIGHTPLAN.ToString());
                sw.WriteLine("AUTO_ALWAYSONTOP={0}", AUTO_ALWAYSONTOP.ToString());
                sw.WriteLine("AUTO_TRASPONDER={0}", AUTO_TRASPONDER.ToString());
                sw.WriteLine("IVAO_FP_URL={0}", IVAO_FP_URL);
                sw.WriteLine("CURRENT_CHECKLIST={0}", CURRENT_CHECKLIST);
                sw.Flush();
                sw.Close();

            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
        }

        public static string CALLSIGN
        {
            get 
            {
                return callsign;
            }
            set
            {
                callsign = value;
            }
        }

        public static string VA_ID
        {
            get
            {
                return vaId;
            }
            set
            {
                vaId = value;
            }
        }

        public static bool AUTOLOAD_FLIGHTPLAN
        {
            get
            {
                return autoLoadFlightPlan;
            }
            set
            {
                autoLoadFlightPlan = value;
            }
        }

        public static bool AUTO_ALWAYSONTOP
        {
            get
            {
                return autoAlwaysOnTop;
            }
            set
            {
                autoAlwaysOnTop = value;
            }
        }

        public static bool AUTO_TRASPONDER
        {
            //per issue 63
            get
            {
                return autoTrasponder;
            }
            set
            {
                autoTrasponder = value;
            }
        }

        public static string IVAO_FP_URL 
        { 
            get 
            {
                return ivaoFpUrl;
            }
            set
            {
                ivaoFpUrl = value;
            }
        }

        public static string CURRENT_CHECKLIST
        {
            get
            {
                return currentChecklist;
            }
            set
            {
                currentChecklist = value;
            }
        }


        private static string CfgFileFullPath
        {
            get
            {
                string tmp = Assembly.GetExecutingAssembly().Location;

                return Path.Combine(tmp.Substring(0, tmp.LastIndexOf(Path.DirectorySeparatorChar)), CONFIG_FILE_NAME);
            }
        }

    }
}

