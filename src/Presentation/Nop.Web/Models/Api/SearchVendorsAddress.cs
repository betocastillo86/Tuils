using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.Api
{
    /// <summary>
    /// Objeto con pocas propiedades que se retorna en los filtros de usuarios
    /// </summary>
    public class SearchVendorsAddress
    {
        public int id { get; set; }

        public double lat { get; set; }

        public double lon { get; set; }

        //public string phone { get; set; }

        //public string name { get; set; }

        public string address { get; set; }

        public int vId { get; set; }

        public int vType { get; set; }

        public string vName { get; set; }

        public string seName { get; set; }
    }
}