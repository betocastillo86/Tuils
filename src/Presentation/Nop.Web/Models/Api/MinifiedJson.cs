using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.Api
{
    [Serializable]
    public struct MinifiedJson
    {
        public MinifiedJson(string id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
        
        string Id;
        string Name;
    }
}