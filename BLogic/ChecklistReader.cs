using System;
using System.Collections.Generic;
using System.Text;
using Castellari.IVaPS.Model;
using System.Xml;
using System.IO;

namespace Castellari.IVaPS.BLogic
{
    /// <summary>
    /// Classe di utility per la lettura delle checklist
    /// </summary>
    public class ChecklistReader
    {
        private static string CHECKLIST_RELATIVE_PATH = @".";
        private static string CHECKLIST_FILE_EXTENSION = "chklst";

        /// <summary>
        /// Ritorna la lista delle checklist disponibili
        /// </summary>
        /// <returns></returns>
        public static string[] ReadAvailableChecklists()
        {
            List<string> toBeRet = new List<string>();
            string[] fileNames = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), CHECKLIST_RELATIVE_PATH));
            foreach (string filename in fileNames)
            {
                if (filename.EndsWith(CHECKLIST_FILE_EXTENSION))
                {
                    toBeRet.Add(filename.Substring(filename.LastIndexOf(Path.DirectorySeparatorChar)+1, filename.LastIndexOf(".") - filename.LastIndexOf(Path.DirectorySeparatorChar) - 1));
                }
            }

            return toBeRet.ToArray(); ;
        }

        /// <summary>
        /// Selezionata la checklist la costruisce OOP
        /// </summary>
        /// <param name="checkListName"></param>
        /// <returns></returns>
        public static Checklist ReadChecklist(string checkListName)
        {
            FileInfo fi = new FileInfo(Path.Combine(Path.Combine(Directory.GetCurrentDirectory(),CHECKLIST_RELATIVE_PATH) , checkListName + "." + CHECKLIST_FILE_EXTENSION));
            if (!fi.Exists) return null;

            XmlTextReader reader = new XmlTextReader( Path.Combine(Path.Combine(Directory.GetCurrentDirectory(),CHECKLIST_RELATIVE_PATH) , checkListName + "." + CHECKLIST_FILE_EXTENSION));
            Checklist toBeRet = new Checklist();
            toBeRet.Phases = new List<ChecklistPhase>();

            while (reader.Read())
            {
                XmlNodeType nType = reader.NodeType;
                if (nType == XmlNodeType.Element)
                {
                    if (reader.Name == "aircraft")
                    {
                        toBeRet.AircraftIcaoCode = reader.GetAttribute("icao");
                        toBeRet.Vr = reader.GetAttribute("Vr");
                        toBeRet.Vs = reader.GetAttribute("Vs");
                        toBeRet.Vapp = reader.GetAttribute("Vapp");
                        toBeRet.Vf0 = reader.GetAttribute("Vf0");
                        toBeRet.Vldg = reader.GetAttribute("Vldg");
                        toBeRet.Vne = reader.GetAttribute("Vne");
                    }
                    if (reader.Name == "phase")
                    {
                        ChecklistPhase phase = new ChecklistPhase();
                        phase.PhaseName = reader.GetAttribute("desc");
                        phase.Items = new List<ChecklistItem>();
                        toBeRet.Phases.Add(phase);
                    }
                    if (reader.Name == "chklstItem")
                    {
                        ChecklistItem item = new ChecklistItem();
                        item.Description = reader.GetAttribute("desc");
                        item.Value = reader.GetAttribute("value");
                        if (reader.GetAttribute("delay") != null)
                        {
                            item.Delay = int.Parse(reader.GetAttribute("delay"));
                        }
                        toBeRet.Phases[toBeRet.Phases.Count - 1].Items.Add(item);
                    }
                }
            }
            reader.Close();//issue 70
            return toBeRet;
        }
    }
}
