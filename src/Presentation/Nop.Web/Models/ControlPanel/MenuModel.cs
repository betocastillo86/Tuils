using Nop.Core.Domain.ControlPanel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.ControlPanel
{
    public class MenuModel
    {
        public List<ControlPanelModule> Modules { get; set; }

        public string SelectedParentModule { get; set; }

        public string SelectedModule { get; set; }


    }
}