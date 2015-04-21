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
    }
}
