using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Nop.Web.Extensions
{
    public static class ListExtenstions
    {
        /// <summary>
        /// Retorna una cadena con la lista de los items separados por el separador enviado
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string ToStringSeparatedBy<T>(this List<T> list, string separator = ",")
        {
            var result = new StringBuilder();
            foreach (var item in list)
	        {
		        if(result.Length > 0) 
                    result.AppendFormat("{0}{1}", separator, item.ToString());
                else
                    result.Append(item.ToString());
	        }

            return result.ToString();
        }

    }
}