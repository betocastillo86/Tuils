using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Catalog
{
    public class LeftFeaturedByVendorModel
    {
        /// <summary>
        /// Productos que le quedan disponibles para redes sociales al usuario
        /// </summary>
        public int SocialNetworkLeft { get; set; }

        /// <summary>
        /// Productos que tiene para destacar en redes social el plan del usuario
        /// </summary>
        public int SocialNetworkByPlan { get; set; }

        /// <summary>
        /// Productos que le quedan disponibles para HOME al usuario
        /// </summary>
        public int HomePageLeft { get; set; }

        /// <summary>
        /// Productos que tiene para destacar en HOME el plan del usuario
        /// </summary>
        public int HomePageByPlan { get; set; }


        /// <summary>
        /// Productos que le quedan disponibles para SLIDERS al usuario
        /// </summary>
        public int SlidersLeft { get; set; }

        /// <summary>
        /// Productos que tiene para destacar en SLIDERS el plan del usuario
        /// </summary>
        public int SlidersByPlan { get; set; }


        /// <summary>
        /// Productos que le quedan disponibles al usuario
        /// </summary>
        public int ProductsLeft { get; set; }

        /// <summary>
        /// Productos que tiene el plan del usuario
        /// </summary>
        public int ProductsByPlan { get; set; }


        public int DisplayOrder { get; set; }
    }
}
