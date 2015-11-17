using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Vendors;
using Nop.Services.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Vendors
{
    public static class VendorExtensions
    {
        /// <summary>
        /// Retorna el plan del vendedor, si no tiene plan retorna el gratis dependiendo el tipo de usuario
        /// </summary>
        /// <param name="vendor"></param>
        /// <param name="productService"></param>
        /// <param name="planSettings"></param>
        /// <returns></returns>
        public static PlanModel GetCurrentPlan(this Vendor vendor, IProductService productService, PlanSettings planSettings)
        {
            //Carga por defecto el plan gratis
            var planId = vendor.VendorType == VendorType.Market ? planSettings.PlanStoresFree : planSettings.PlanProductsFree;
            //Si tiene plan lo carga
            if (vendor.CurrentOrderPlan != null && vendor.PlanExpiredOnUtc > DateTime.UtcNow)
                planId = vendor.CurrentOrderPlan.OrderItems.FirstOrDefault().ProductId;

            return productService.GetPlanById(planId);
        }

        /// <summary>
        /// Muestra si un usuario tiene un plan activo o no
        /// </summary>
        /// <param name="vendor"></param>
        /// <returns></returns>
        public static bool HasActivePlan(this Vendor vendor)
        {
            return vendor.CurrentOrderPlanId.HasValue && vendor.PlanExpiredOnUtc > DateTime.UtcNow;
        }


    }
}
