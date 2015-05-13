using Nop.Core.Domain.Directory;
using Nop.Web.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.ControlPanel
{
    public class OfficesModel
    {
        public int VendorId { get; set; }
        
        public IList<StateProvince> States { get; set; }

    }
}