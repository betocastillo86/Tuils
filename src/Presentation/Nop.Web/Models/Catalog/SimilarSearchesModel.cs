using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.Catalog
{
    public class SimilarSearchesModel
    {
        public string Title { get; set; }

        public string TitleOfTitle { get; set; }

        public bool Enable { get; set; }
        public List<string> Searches { get; set; }
    }
}