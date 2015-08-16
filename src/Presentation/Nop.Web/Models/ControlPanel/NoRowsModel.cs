using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.ControlPanel
{
    public class NoRowsModel
    {
        public NoRowsModel()
        {
            //List = new List<T>();
        }
        
        public IList List { get; set; }

        public string ResourceMessage { get; set; }

        public bool ShowSimilarSearches { get; set; }
        public bool ShowPublishProduct { get; set; }
    }
}