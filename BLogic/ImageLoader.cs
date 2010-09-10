using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Castellari.IVaPS.BLogic
{
    /// <summary>
    /// CLasse di utility per la gestione delle immagini per l'ImageViewerForm
    /// </summary>
    public class ImageLoader
    {
        public static string IMAGE_FOLDER_RELATIVE_PATH = "images";
        private static string IMAGE_EXTENSION = "png";

        public static string[] FindImages()
        {
            List<string> toBeRet = new List<string>();
            try
            {
                string[] fileNames = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), IMAGE_FOLDER_RELATIVE_PATH));
                foreach (string filename in fileNames)
                {
                    if (filename.EndsWith(IMAGE_EXTENSION))
                    {
                        toBeRet.Add(filename);
                    }
                }
                toBeRet.Sort();
                return toBeRet.ToArray();
            }
            catch
            {
                return new string[0];
            }
        }
    }
}
