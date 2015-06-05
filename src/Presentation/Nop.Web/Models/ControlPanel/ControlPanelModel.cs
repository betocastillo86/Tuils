using Nop.Core.Domain.ControlPanel;
using Nop.Web.Models.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Models.ControlPanel
{
    public class ControlPanelModel
    {
        public List<ControlPanelModule> Modules { get; set; }

        public int PublishedProducts { get; set; }

        public int SoldProducts { get; set; }

        public int NumRatings { get; set; }

        public double AvgRating { get; set; }

        public int NewMessages { get; set; }

        public int UnansweredQuestions { get; set; }


        public MyAccountModel    Customer { get; set; }

    }
}