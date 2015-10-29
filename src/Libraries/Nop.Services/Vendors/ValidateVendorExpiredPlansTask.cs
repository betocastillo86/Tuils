using Nop.Core.Data;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Vendors;
using Nop.Services.Catalog;
using Nop.Services.Messages;
using Nop.Services.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Vendors
{
    public class ValidateVendorExpiredPlansTask : ITask
    {
        private readonly IVendorService _vendorService;
        private readonly IWorkflowMessageService _workflowMessageService;
        private readonly IRepository<Vendor> _vendorRepository;
        private readonly TuilsSettings _tuilsSettings;


        public ValidateVendorExpiredPlansTask(IVendorService vendorService,
            IRepository<Vendor> vendorRepository,
            IWorkflowMessageService workflowMessageService,
            TuilsSettings tuilsSettings)
        {
            _vendorService = vendorService;
            _vendorRepository = vendorRepository;
            _workflowMessageService = workflowMessageService;
            _tuilsSettings = tuilsSettings;
        }


        public void Execute()
        {
            ExecuteAlmostFinished();
            ExecuteFinished();
        }


        private void ExecuteFinished()
        {
            var vendorsWithoutNotification = _vendorRepository.Table
                .Where(v => v.PlanExpiredOnUtc < DateTime.UtcNow && !v.PlanFinishedMessageSent)
                .ToList();

            //Anvia todas las notificaciones pendientes
            foreach (var vendor in vendorsWithoutNotification)
            {
                _workflowMessageService.SendVendorPlanFinishedNotificationMessage(vendor, 2);
                //Actualiza el producto y deja el mensjae como enviado
                vendor.PlanFinishedMessageSent = true;
                _vendorService.UpdateVendor(vendor);
            }
        }

        /// <summary>
        /// Ejecuta los que todavía no han sido cerrados
        /// </summary>
        private void ExecuteAlmostFinished()
        {
            var dateFromSearch = DateTime.UtcNow.AddDays(_tuilsSettings.SendMessageExpirationProductDaysBefore);
            //Consulta los vendors que se les venza el plan en los siguientes dias y que el correo no haya sido envaido
            var vendorsWithoutNotification = _vendorRepository.Table.Where(v => v.PlanExpiredOnUtc < dateFromSearch && !v.ExpirationPlanMessageSent).ToList(); 

            //Envia todas las notificaciones pendientes
            foreach (var vendor in vendorsWithoutNotification)
            {
                _workflowMessageService.SendVendorPlanExpirationNotificationMessage(vendor, _tuilsSettings.SendMessageExpirationProductDaysBefore, 2);
                //Actualiza el vendor y deja el mensjae como enviado
                vendor.ExpirationPlanMessageSent= true;
                _vendorService.UpdateVendor(vendor);
            }
        }
    }
}
