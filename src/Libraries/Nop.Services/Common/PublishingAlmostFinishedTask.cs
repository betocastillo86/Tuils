using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Services.Catalog;
using Nop.Services.Messages;
using Nop.Services.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Common
{
    public class PublishingAlmostFinishedTask : ITask
    {
        private readonly IProductService _productService;
        private readonly IStoreContext _storeContext;
        private readonly IWorkflowMessageService _workflowMessageService;
        private readonly TuilsSettings _tuilsSettings;

        public PublishingAlmostFinishedTask(IProductService productService, 
            IWorkflowMessageService workflowMessageService,
            TuilsSettings tuilsSettings,
            IStoreContext storeContext)
        {
            this._productService = productService;
            this._workflowMessageService = workflowMessageService;
            this._tuilsSettings = tuilsSettings;
            this._storeContext = storeContext;
        }

        public void Execute()
        {
            ExecuteAlmostFinished();
            ExecuteFinished();
        }

        private void ExecuteFinished()
        {
            var productsWithoutNotification = this._productService.GetProductsFinishedPublishing();

            //Anvia todas las notificaciones pendientes
            foreach (var product in productsWithoutNotification)
            {
                //Solo realiza el envio cuando el vendor no tiene plan o la fecha de expiración del plan es menor a la del producto
                if (!product.Vendor.PlanExpiredOnUtc.HasValue || product.Vendor.PlanExpiredOnUtc.Value.AddDays(1) < product.AvailableEndDateTimeUtc)
                    _workflowMessageService.SendProductFinishedNotificationMessage(product, 2);

                //Actualiza el producto y deja el mensjae como enviado
                product.PublishingFinishedMessageSent = true;
                _productService.UpdateProduct(product);
            }
        }

        /// <summary>
        /// Ejecuta los que todavía no han sido cerrados
        /// </summary>
        private void ExecuteAlmostFinished()
        {
            var productsWithoutNotification = this._productService.GetProductsAlmostToFinishPublishing(_tuilsSettings.SendMessageExpirationProductDaysBefore, false);

            //Anvia todas las notificaciones pendientes
            foreach (var product in productsWithoutNotification)
            {
                //Solo realiza el envio cuando el vendor no tiene plan o la fecha de expiración del plan es menor a la del producto
                if (!product.Vendor.PlanExpiredOnUtc.HasValue || product.Vendor.PlanExpiredOnUtc.Value.AddDays(1) < product.AvailableEndDateTimeUtc)
                    _workflowMessageService.SendProductExpirationNotificationMessage(product,_tuilsSettings.SendMessageExpirationProductDaysBefore,  2);
                //Actualiza el producto y deja el mensjae como enviado
                product.ExpirationMessageSent = true;
                _productService.UpdateProduct(product);
            }
        }
    }
}
