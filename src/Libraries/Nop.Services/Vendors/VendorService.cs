using System;
using System.Linq;
using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.Vendors;
using Nop.Services.Events;
using Nop.Services.Customers;
using Nop.Services.Media;
using Nop.Core.Domain.Media;
using System.IO;
using Nop.Services.Logging;
using System.Collections.Generic;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Customers;
using Nop.Services.Catalog;
using Nop.Core.Domain.Orders;
using Nop.Services.Common;
using Nop.Core.Domain.Common;

namespace Nop.Services.Vendors
{
    /// <summary>
    /// Vendor service
    /// </summary>
    public partial class VendorService : IVendorService
    {
        #region Fields

        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : product ID
        /// </remarks>
        private const string VENDORS_BY_ID_KEY = "Nop.vendor.id-{0}";

        private readonly IRepository<Vendor> _vendorRepository;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<ProductReview> _productReviewRepository;
        private readonly IRepository<VendorReview> _vendorReviewRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<SpecialCategoryVendor> _specialCategoryVendorRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly ICustomerService _customerService;
        private readonly IPictureService _pictureService;
        private readonly VendorSettings _vendorSettings;
        private readonly ILogger _logger;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="vendorRepository">Vendor repository</param>
        /// <param name="eventPublisher">Event published</param>
        public VendorService(IRepository<Vendor> vendorRepository,
            IEventPublisher eventPublisher,
            ICustomerService customerService,
            VendorSettings vendorSettings,
            IPictureService pictureService,
            ILogger logger,
            IRepository<SpecialCategoryVendor> specialCategoryVendorRepository,
            IRepository<Category> categoryRepository,
            IRepository<ProductReview> productReviewRepository,
            IRepository<Product> productRepository,
            IRepository<Customer> customerRepository,
            IRepository<VendorReview> vendorReviewRepository)
        {
            this._vendorRepository = vendorRepository;
            this._eventPublisher = eventPublisher;
            this._customerService = customerService;
            this._vendorSettings = vendorSettings;
            this._pictureService = pictureService;
            this._logger = logger;
            this._specialCategoryVendorRepository = specialCategoryVendorRepository;
            this._categoryRepository = categoryRepository;
            this._productReviewRepository = productReviewRepository;
            this._productRepository = productRepository;
            this._customerRepository = customerRepository;
            this._vendorReviewRepository = vendorReviewRepository;
        }

        #endregion

        #region Methods
        
        /// <summary>
        /// Gets a vendor by vendor identifier
        /// </summary>
        /// <param name="vendorId">Vendor identifier</param>
        /// <param name="includeImages">Incluye el logo y el fondo del vendor</param>
        /// <returns>Vendor</returns>
        public virtual Vendor GetVendorById(int vendorId, bool includeImages = false)
        {
            if (vendorId == 0)
                return null;

            var vendor = _vendorRepository.GetById(vendorId);

            if (includeImages && vendor.PictureId.HasValue)
            {
               vendor.Picture = _pictureService.GetPictureById(vendor.PictureId.Value);
               if (vendor.BackgroundPictureId.HasValue) vendor.BackgroundPicture = _pictureService.GetPictureById(vendor.BackgroundPictureId.Value);
            }

            return vendor;
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
            int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false, bool? showOnHomePage = null, bool? withPlan = null, 
            VendorType? vendorType = null, string email = null, VendorOrderBy order = VendorOrderBy.Name)
        {
            var query = _vendorRepository.Table;
            if (!String.IsNullOrWhiteSpace(name))
                query = query.Where(v => v.Name.Contains(name));
            if (!showHidden)
                query = query.Where(v => v.Active);

            if (showOnHomePage.HasValue)
                query = query.Where(v => v.ShowOnHomePage == showOnHomePage.Value);

            if (withPlan.HasValue && withPlan.Value)
                query = query.Where(v => v.CurrentOrderPlanId != null);

            if (!string.IsNullOrEmpty(email))
                query = query.Where(v => v.Email.Contains(email));

            if (vendorType.HasValue)
            {
                int vendorTypeId = Convert.ToInt32(vendorType);
                query = query.Where(v => v.VendorTypeId == vendorTypeId);
            }
                

            query = query.Where(v => !v.Deleted);

            switch (order)
            {
                case VendorOrderBy.Id:
                    query = query.OrderByDescending(v => v.Id);
                    break;
                case VendorOrderBy.Name:
                    query = query.OrderBy(v => v.DisplayOrder).ThenBy(v => v.Name);
                    break;
            }

            var vendors = new PagedList<Vendor>(query, pageIndex, pageSize);
            return vendors;
        }

        /// <summary>
        /// Inserts a vendor
        /// </summary>
        /// <param name="vendor">Vendor</param>
        public virtual void InsertVendor(Vendor vendor, bool loadDefaultCover = true, bool loadDefaultLogo = true)
        {
            if (vendor == null)
                throw new ArgumentNullException("vendor");

            if (vendor.PageSize == 0)
                vendor.PageSize = _vendorSettings.DefaultPageSize;

            if (loadDefaultCover)
                vendor.BackgroundPictureId = _vendorSettings.GetRandomCover();

            _vendorRepository.Insert(vendor);

            //event notification
            _eventPublisher.EntityInserted(vendor);
        }

        /// <summary>
        /// Actualiza unicamente los campos del header
        /// </summary>
        /// <param name="vendor">Nuevos datos del header</param>
        /// <returns></returns>
        public virtual bool UpdateVendorHeader(Vendor vendor)
        {
            var actual = GetVendorById(vendor.Id);
            actual.Name = vendor.Name;
            actual.Description = vendor.Description;
            actual.EnableShipping = vendor.EnableShipping;
            actual.EnableCreditCardPayment = vendor.EnableCreditCardPayment;
            return UpdateVendor(actual);
        }

        /// <summary>
        /// Actualiza la posici�n de fondo del vendedor en el sitio
        /// </summary>
        /// <param name="vendorId"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public virtual bool UpdateBackgroundPosition(int vendorId, int position)
        {
            var actual = GetVendorById(vendorId);
            actual.BackgroundPosition = position;
            return UpdateVendor(actual);
        }


        /// <summary>
        /// Updates the vendor
        /// </summary>
        /// <param name="vendor">Vendor</param>
        public virtual bool UpdateVendor(Vendor vendor)
        {
            if (vendor == null)
                throw new ArgumentNullException("vendor");

            bool success = _vendorRepository.Update(vendor) > 0;

            //event notification
            _eventPublisher.EntityUpdated(vendor);

            return success;
        }

        /// <summary>
        /// Retorna el vendor asociado a un cliente. 
        /// Si el cliente no tiene asociado ninguno se tiene la opci�n de crear uno por defecto basado en los datos del cliente
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
                //Si esta habilitada la creaci�n si no existe clona los datos del usuario en el vendedor
                vendor = new Vendor();
                vendor.Name = customer.GetFullName();
                vendor.Email = customer.Email;
                vendor.Description = string.Empty;
                vendor.Active = true;
                //Siempre el vendor subtype es usuario ya que cuando una tienda o taller se registran, se les crea un vendor por defecto
                vendor.VendorSubTypeId = Convert.ToInt32(VendorSubType.User);
                InsertVendor(vendor);

                //Actualiza los datos del cliente con el codigo del vendedor
                customer.VendorId = vendor.Id;
                _customerService.UpdateCustomer(customer);
            }

            return vendor ?? new Vendor();
        }

        #endregion

        #region Pictures
        /// <summary>
        /// Permite cargar el archivo de un vendor y actualizarlo como la foto principal o la de fondo
        /// </summary>
        /// <param name="dataFile">datos del archivo</param>
        /// <param name="extension">extensi�n del archivo</param>
        /// <param name="isMainPicture">True: es la imagen principal False: es el fondo</param>
        public bool UpdatePicture(int vendorId, byte[] dataFile, string extension, bool isMainPicture)
        {
            var vendor = GetVendorById(vendorId);
            int? pictureId = null;
            if(isMainPicture)
                 pictureId = vendor.PictureId;
            else
                pictureId = vendor.BackgroundPictureId;


            try
            {
                string mimeType = _pictureService.GetContentTypeFromExtension(extension);
                string seoName = Seo.SeoExtensions.GetSeName(vendor.Name);

                //Si la foto ya est� asignada la actualiza, sino la crea
                Picture picture = null;
                //No debe actualizar debido al nuevo funcionamiento
                bool pictureExisted = false;

                picture = _pictureService.InsertPicture(dataFile, mimeType, seoName, true);

                if (isMainPicture)
                    vendor.PictureId = picture.Id;
                else
                    vendor.BackgroundPictureId = picture.Id;

                
                //Si la imagen no existia actualiza
                //Si la imagen existia pero es el Cover, debe actualizar la posici�n del fondo
                if (!pictureExisted || (!isMainPicture && vendor.BackgroundPosition != 0))
                {
                    //Si es el cover actualiza la posici�n del fondo
                    if (!isMainPicture)
                        vendor.BackgroundPosition = 0;

                    return UpdateVendor(vendor);
                }
                else
                    return true;
                    
            }
            catch (Exception e)
            {
                _logger.Error(e.ToString(), e);
                return false;
            }
            
        }
        #endregion

        #region Special Categories
        /// <summary>
        /// Retorna el listado de categorias asociadas con el vendedor
        /// </summary>
        /// <param name="vendorId"></param>
        /// <returns></returns>
        public IList<SpecialCategoryVendor> GetSpecialCategoriesByVendorId(int vendorId)
        {
            
            var query = from sc in _specialCategoryVendorRepository.Table 
                        join c in _categoryRepository.Table on sc.CategoryId equals c.Id
                        where sc.VendorId == vendorId
                        select sc;

            return query.ToList();
        }

        /// <summary>
        /// Inserta o actualiza todas las categorias especiales relacionadas con un vendedor
        /// </summary>
        /// <param name="specialCategories"></param>
        /// <returns></returns>
        public bool InsertUpdateVendorSpecialCategories(int vendorId, IList<SpecialCategoryVendor> specialCategories)
        {
            //Consulta las categorias anteriores y debe realizar una comparaci�n de cuales son nuevas y eliminadas
            var oldSpecialCategories = GetSpecialCategoriesByVendorId(vendorId);

            try
            {
                //Recorre todas las nuevas categorias y las va insertando
                foreach (var newCategory in specialCategories)
                {
                    //busca la categoria nueva en las anteriores
                    var oldCategory = oldSpecialCategories.FirstOrDefault(c => c.SpecialType == newCategory.SpecialType && c.CategoryId == newCategory.Id);
                    //Si la categoria no existe la crea
                    if (oldCategory == null)
                    {
                        InsertSpecialCategoryVendor(newCategory);
                    }

                    //Va removiendo las categorias que va revisando
                    oldSpecialCategories.Remove(oldCategory);
                }

                foreach (var oldCategory in oldSpecialCategories)
                {
                    _specialCategoryVendorRepository.Delete(oldCategory);
                }

                return true;
            }
            catch (Exception e)
            {
                _logger.Error(e.ToString(), e);
                return false;
            }
        }

        /// <summary>
        /// Inserta una categoria especial relacionada con el vendedor
        /// </summary>
        /// <param name="vendorId"></param>
        /// <param name="categoryId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool InsertSpecialCategoryVendor(SpecialCategoryVendor specialCategory)
        {
            try
            {
                _specialCategoryVendorRepository.Insert(specialCategory);
                return true;
            }
            catch (Exception e)
            {
                _logger.Error(e.ToString(), e);
                return false;
            }
            
        }
        #endregion

        #region Reviews
        /// <summary>
        /// Retorna el listado de reviews hechos a los productos de un vendedor
        /// </summary>
        /// <returns></returns>
        public IList<IReview> GetReviewsByVendorId(int vendorId)
        {
            //var queryProduct = (from r in _productReviewRepository.Table
            //                       //join p in _vendorRepository.Table on r.ProductId equals p.Id
            //                       join c in _customerRepository.Table on r.CustomerId equals c.Id
            //                       where r.IsApproved

            //                       select r).ToList<IReview>();

            var queryVendor = (from r in _vendorReviewRepository.Table
                                //join p in _vendorRepository.Table on r.ProductId equals p.Id
                                join c in _customerRepository.Table on r.CustomerId equals c.Id
                                where r.IsApproved && r.VendorId == vendorId
                                select r).ToList<IReview>();

            return queryVendor.ToList();
        }

        /// <summary>
        /// Actualiza los valores de AvgRating y NumRating del vendor dependiendo de los reviews recibidos
        /// </summary>
        /// <param name="vendorId">Vendedor a ser actualziado</param>
        public void UpdateRatingsTotal(int vendorId)
        {
            if (vendorId <= 0)
                return;

            var vendor = GetVendorById(vendorId);
            if (vendor != null)
            {
                var reviews = GetReviewsByVendorId(vendorId);

                vendor.NumRatings = reviews.Count;
                vendor.AvgRating = reviews.Count > 0 ? reviews.Average(r => r.Rating) : 0;
                UpdateVendor(vendor);
            }
        }




        #endregion

        public Vendor AddPlanToVendor(Order order)
        {
            if(order == null)
                throw new ArgumentNullException("order");

            //consulta el vendor de la orden
            var vendor = GetVendorById(order.Customer.VendorId);

            //Valida que no este intentando aplicar la orden dos veces
            if(vendor.CurrentOrderPlanId.HasValue && vendor.CurrentOrderPlanId.Value == order.Id)
                throw new NopException("No se puede aplicar la misma orden del plan dos veces");

            //Consulta el plan que desea agregar
            var selectedPlan = order.OrderItems.FirstOrDefault().Product;

            var planSettings = Nop.Core.Infrastructure.EngineContext.Current.Resolve<PlanSettings>();
            var planDays = Convert.ToInt32(selectedPlan.ProductSpecificationAttributes.FirstOrDefault(p => p.SpecificationAttributeOption.SpecificationAttributeId == planSettings.SpecificationAttributePlanDays).SpecificationAttributeOption.Name);
            //Si tiene asignada la caracteristica que sale en el home la aplica para el vendor
            bool showOnHome = selectedPlan.ProductSpecificationAttributes.FirstOrDefault(p => p.SpecificationAttributeOption.SpecificationAttributeId == planSettings.SpecificationAttributeIdHomePage) != null;


            //Si el vendedor ya tenia un plan previamente calcula la fecha de cierre
            //y el plan que tiene actualmente no ha expirado, le suma los dias
            if (vendor.CurrentOrderPlanId.HasValue && vendor.PlanExpiredOnUtc > DateTime.UtcNow)
            {
                vendor.PlanExpiredOnUtc = vendor.PlanExpiredOnUtc.Value.AddDaysToPlan(planDays);
            }
            else
            {
                vendor.PlanExpiredOnUtc = DateTime.UtcNow.AddDaysToPlan(planDays);
            }

            vendor.PlanFinishedMessageSent = false;
            vendor.ExpirationPlanMessageSent = false;
            vendor.ShowOnHomePage = showOnHome;
            vendor.CurrentOrderPlanId = order.Id;

            UpdateVendor(vendor);


            return vendor;
        }

        #region Product reviews

        /// <summary>
        /// Gets all product reviews
        /// </summary>
        /// <param name="customerId">Customer identifier; 0 to load all records</param>
        /// <param name="approved">A value indicating whether to content is approved; null to load all records</param> 
        /// <param name="fromUtc">Item creation from; null to load all records</param>
        /// <param name="toUtc">Item item creation to; null to load all records</param>
        /// <param name="message">Search title or review text; null to load all records</param>
        /// <returns>Reviews</returns>
        public virtual IList<VendorReview> GetAllVendorReviews(int? customerId = null, bool? approved = null,
            DateTime? fromUtc = null, DateTime? toUtc = null,
            string message = null, int? vendorId = null)
        {
            var query = _vendorReviewRepository.Table;
            if (approved.HasValue)
                query = query.Where(c => c.IsApproved == approved);
            if (customerId > 0)
                query = query.Where(c => c.CustomerId == customerId);
            if (fromUtc.HasValue)
                query = query.Where(c => fromUtc.Value <= c.CreatedOnUtc);
            if (toUtc.HasValue)
                query = query.Where(c => toUtc.Value >= c.CreatedOnUtc);
            if (vendorId.HasValue)
                query = query.Where(c => vendorId.Value == c.VendorId);
            if (!String.IsNullOrEmpty(message))
                query = query.Where(c => c.Title.Contains(message) || c.ReviewText.Contains(message));

            query = query.OrderBy(c => c.CreatedOnUtc);
            var content = query.ToList();
            return content;
        }

        /// <summary>
        /// Gets product review
        /// </summary>
        /// <param name="productReviewId">Product review identifier</param>
        /// <returns>Product review</returns>
        public virtual VendorReview GetVendorReviewById(int vendorReviewId)
        {
            if (vendorReviewId == 0)
                return null;

            return _vendorReviewRepository.GetById(vendorReviewId);
        }

        /// <summary>
        /// Deletes a product review
        /// </summary>
        /// <param name="vendorReview">Product review</param>
        public virtual void DeleteVendorReview(VendorReview vendorReview)
        {
            if (vendorReview == null)
                throw new ArgumentNullException("vendorReview");

            _vendorReviewRepository.Delete(vendorReview);

            //_cacheManager.RemoveByPattern(VENDORS_BY_ID_KEY);
        }

        public virtual bool CustomerHasVendorReview(int customerId, int vendorId)
        {
            var reviews = GetAllVendorReviews(customerId: customerId, vendorId:vendorId);
            return reviews != null && reviews.Count > 1;
        }

        #endregion
    }
}