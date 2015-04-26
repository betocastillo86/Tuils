using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.Api
{
    public class SpecificationAttributeModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<MinifiedJson> Options { get; set; }

    }
}