using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.Api
{
    /// <summary>
    /// Estructura de los errores que pasan en el lado del cliente y que son reportados
    /// </summary>
    public class ErrorJavascriptModel
    {
        public string Message { get; set; }

        public string File { get; set; }

        public int Line { get; set; }

        public int Column { get; set; }

        public string Url { get; set; }

        public string Stack { get; set; }
    }
}