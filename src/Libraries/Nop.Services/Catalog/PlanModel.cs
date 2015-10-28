using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Catalog
{
    /// <summary>
    /// Clase de ayuda para identificar la estructura de un plan
    /// </summary>
    public class PlanModel
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public int NumProducts { get; set; }

        public int NumProductsOnHome { get; set; }

        public int NumProductsOnSliders { get; set; }

        public int NumProductsOnSocialNetworks { get; set; }

        public bool ShowOnHomePage { get; set; }

        public bool ShowOnSliders { get; set; }

        public bool ShowOnSocialNetworks { get; set; }
    }
}
