using System;
using System.Linq;
using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.Vendors;
using Nop.Services.Events;
using Nop.Services.Customers;

namespace Nop.Services.Vendors
{
    /// <summary>
    /// Vendor service
    /// </summary>
    public partial class VendorService : IVendorService
    {
        #region Fields

        private readonly IRepository<Vendor> _vendorRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly ICustomerService _customerService;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="vendorRepository">Vendor repository</param>
        /// <param name="eventPublisher">Event published</param>
        public VendorService(IRepository<Vendor> vendorRepository,
            IEventPublisher eventPublisher,
            ICustomerService customerService)
        {
            this._vendorRepository = vendorRepository;
            this._eventPublisher = eventPublisher;
            this._customerService = customerService;
        }

        #endregion

        #region Methods
        
        /// <summary>
        /// Gets a vendor by vendor identifier
        /// </summary>
        /// <param name="vendorId">Vendor identifier</param>
        /// <returns>Vendor</returns>
        public virtual Vendor GetVendorById(int vendorId)
        {
            if (vendorId == 0)
                return null;

            return _vendorRepository.GetById(vendorId);
        }

        /// <summary>
        /// Delete a vendor
        /// </summary>
        /// <param name="vendor">Vendor</param>
        public virtual void DeleteVendor(Vendor vendor)
        {
            if (vendor == null)
                throw new ArgumentNullException("vendor");

            vendor.Deleted = true;
            UpdateVendor(vendor);
        }

        /// <summary>
        /// Gets all vendors
        /// </summary>
        /// <param name="name">Vendor name</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Vendors</returns>
        public virtual IPagedList<Vendor> GetAllVendors(string name = "",
            int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false)
        {
            var query = _vendorRepository.Table;
            if (!String.IsNullOrWhiteSpace(name))
                query = query.Where(v => v.Name.Contains(name));
            if (!showHidden)
                query = query.Where(v => v.Active);
            query = query.Where(v => !v.Deleted);
            query = query.OrderBy(v => v.DisplayOrder).ThenBy(v => v.Name);

            var vendors = new PagedList<Vendor>(query, pageIndex, pageSize);
            return vendors;
        }

        /// <summary>
        /// Inserts a vendor
        /// </summary>
        /// <param name="vendor">Vendor</param>
        public virtual void InsertVendor(Vendor vendor)
        {
            if (vendor == null)
                throw new ArgumentNullException("vendor");

            _vendorRepository.Insert(vendor);

            //event notification
            _eventPublisher.EntityInserted(vendor);
        }

        /// <summary>
        /// Updates the vendor
        /// </summary>
        /// <param name="vendor">Vendor</param>
        public virtual void UpdateVendor(Vendor vendor)
        {
            if (vendor == null)
                throw new ArgumentNullException("vendor");

            _vendorRepository.Update(vendor);

            //event notification
            _eventPublisher.EntityUpdated(vendor);
        }

        /// <summary>
        /// Retorna el vendor asociado a un cliente. 
        /// Si el cliente no tiene asociado ninguno se tiene la opción de crear uno por defecto basado en los datos del cliente
        /// </summary>
        /// <param name="createIfNotExists">True: Crea el vendor si el cliente no tiene ninguno asociado</param>
        /// <param name="customerId">Id de cliente que sirve como filtro</param>
        /// <returns>Vendor asociado al cliente ya sea creado o no</returns>
        public Vendor GetVendorByCustomerId(int customerId, bool createIfNotExists = false)
        {

            Vendor vendor = null;

            var customer = _customerService.GetCustomerById(customerId);

            if (customer.VendorId > 0)
            {
                vendor = GetVendorById(customer.VendorId);
            }
            else if (createIfNotExists)
            {
                //Si esta habilitada la creación si no existe clona los datos del usuario en el vendedor
                vendor = new Vendor();
                vendor.Name = customer.GetFullName();
                vendor.Email = customer.Email;
                vendor.Description = string.Empty;
                vendor.Active = true;
                InsertVendor(vendor);

                //Actualiza los datos del cliente con el codigo del vendedor
                customer.VendorId = vendor.Id;
                _customerService.UpdateCustomer(customer);
            }

            return vendor ?? new Vendor();
        }

        #endregion
    }
}