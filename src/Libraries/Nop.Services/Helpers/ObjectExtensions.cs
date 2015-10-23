using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Helpers
{
    public static class ObjectExtensions
    {
        public static string ToStringObject(this object reflectObject, string preMessage = null)
        {
            var str = new StringBuilder(string.IsNullOrEmpty(preMessage) ? string.Empty : (preMessage + "\t"));
            foreach (var prop in reflectObject.GetType().GetProperties())
            {
                str.AppendFormat("{0} = {1}\n", prop.Name, prop.GetValue(reflectObject));
            }
            return str.ToString();
        }
    }
}
