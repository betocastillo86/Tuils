using Nop.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Catalog
{
    public class PlanSettings : ISettings
    {
        public int CategoryProductPlansId { get; set; }

        public int CategoryStorePlansId { get; set; }

        public int SpecificationAttributeIdLimitDays { get; set; }

        public int SpecificationAttributeIdPictures { get; set; }

        public int SpecificationAttributeIdDisplayOrder { get; set; }

        public int SpecificationAttributeIdSliders { get; set; }

        public int SpecificationAttributeIdHomePage { get; set; }

        public int SpecificationAttributeIdSocialNetworks { get; set; }
    }
}
