using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Utilities
{
    public class Files
    {
        public static string ReadFile(string fileName)
        {
            string fileContent = string.Empty;
            using (var sr = new StreamReader(fileName))
            {
                fileContent = sr.ReadToEnd();
                sr.Close();
            }

            return fileContent;
        }

        /// <summary>
        /// Retorna la extensión de un archivo de acuerdo a su tipo de contenido
        /// </summary>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public static string GetExtensionByContentType(string contentType)
        {
            string extension = string.Empty;
            switch (contentType)
            {
                case "image/bmp":
                    extension = ".bmp";
                    break;
                case "image/gif":
                    extension = ".gif";
                    break;
                case "image/jpeg":
                    extension = ".jpg";
                    break;
                case "image/png":
                    extension = ".png";
                    break;
                default:
                    break;
            }
            return extension;
        }
    }
}
