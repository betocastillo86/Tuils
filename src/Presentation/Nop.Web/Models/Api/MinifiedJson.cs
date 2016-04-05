using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.Api
{
    [Serializable]
    public struct MinifiedJson
    {
        public MinifiedJson(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
        
        int Id;
        string Name;
    }
}