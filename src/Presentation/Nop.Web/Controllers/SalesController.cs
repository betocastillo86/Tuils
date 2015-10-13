using Nop.Core.Domain.Common;
using Nop.Services.Catalog;
using Nop.Web.Models.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nop.Web.Extensions;
using Nop.Services.Directory;
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Customers;
using Nop.Web.Framework;
using Nop.Core.Caching;
using Nop.Web.Infrastructure.Cache;

namespace Nop.Web.Controllers
{
    public class SalesController : BasePublicController
    {

        #region Fields
        private readonly ICategoryService _categoryService;
        private readonly ISpecificationAttributeService _specificationAttributeService;
        private readonly IStateProvinceService _stateProvinceService;
        private readonly TuilsSettings _tuilsSettings;
        private readonly CatalogSettings _catalogSettings;
        private readonly IWorkContext _workContext;
        private readonly IProductService _productService;
        private readonly PlanSettings _planSettings;
        private readonly ICacheManager _cacheManager;
        #endregion

        #region Ctor

        public SalesController(ICategoryService categoryService,
            ISpecificationAttributeService specificationAttributeService,
            IStateProvinceService stateProvinceService,
            TuilsSettings tuilsSettings,
            IWorkContext workContext,
            CatalogSettings catalogSettings,
            IProductService productService,
            PlanSettings planSettings,
            ICacheManager cacheManager)
        {
            this._categoryService = categoryService;
            this._tuilsSettings = tuilsSettings;
            this._specificationAttributeService = specificationAttributeService;
            this._stateProvinceService = stateProvinceService;
            this._workContext = workContext;
            this._catalogSettings = catalogSettings;
            this._productService = productService;
            this._planSettings = planSettings;
            this._cacheManager = cacheManager;
        }
        #endregion

        // GET: /quiero-vender/
        /// <summary>
        /// Pantalla principal para vender un producto
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var model = new SelectPublishCategoryModel();
            //Solo los talleres pueden publicar servicios
            if (_workContext.CurrentVendor != null)
            {
                model.CanSelectService = _workContext.CurrentVendor.VendorType == Core.Domain.Vendors.VendorType.RepairShop;
                //Si el usuario tiene más productos publicados que el limite, muestra un mensaje de advertencia
                model.HasReachedLimitOfProducts = HasReachedLimitOfProducts();
            }

            return View(model);
        }

        private bool HasReachedLimitOfProducts()
        {
            if (_workContext.CurrentVendor != null)
                //Si el usuario tiene más productos publicados que el limite, muestra un mensaje de advertencia
                return  _productService.HasReachedLimitOfProducts(_workContext.CurrentVendor.Id);
            return false;
        }

        /// <summary>
        /// Publicación de producto simple
        /// </summary>
        /// <returns></returns>
        public ActionResult PublishProduct(int? id)
        {
            var model = GetPublishModel();

            //Tipo de producto servicio
            model.ProductType = ProductTypePublished.Product;
            model.SubSectionTitle = "Productos";

            return View(model);
        }

        /// <summary>
        /// Publicación de producto tipo motocicleta
        /// </summary>
        /// <returns></returns>
        public ActionResult PublishProductBike(int? id)
        {
            var model = GetPublishModel();

            //Caga los colores existentes
            model.ColorOptions = new SelectList(
                _specificationAttributeService
                .GetSpecificationAttributeOptionsBySpecificationAttribute(_tuilsSettings.specificationAttributeColor)
                .Select(a => new { Id = a.Id, Name = a.Name })
                .ToList(),
                "Id",
                "Name");

            model.ConditionOptions = new SelectList(
                _specificationAttributeService
                .GetSpecificationAttributeOptionsBySpecificationAttribute(_tuilsSettings.specificationAttributeCondition)
                .Select(a => new { Id = a.Id, Name = a.Name })
                .ToList(),
                "Id",
                "Name");

            model.AccesoriesOptions = _specificationAttributeService
                .GetSpecificationAttributeOptionsBySpecificationAttribute(_tuilsSettings.specificationAttributeAccesories);

            model.NegotiationOptions = _specificationAttributeService
                .GetSpecificationAttributeOptionsBySpecificationAttribute(_tuilsSettings.specificationAttributeNegotiation);

            //Tipo de producto moto
            model.ProductType = ProductTypePublished.Bike;
            model.SubSectionTitle = "Motos";

            return View("PublishProduct", model);
        }

        /// <summary>
        /// OPción para publicar un servicio
        /// </summary>
        /// <returns></returns>
        public ActionResult PublishProductService(int? id)
        {
            var model = GetPublishModel();
            
            //Tipo de producto moto
            model.ProductType = ProductTypePublished.Service;
            model.SubSectionTitle = "Servicios";

            return View("PublishProduct", model);
        }

        /// <summary>
        /// Funcionalidad que permite seleccionar un plan  
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [SameVendorProduct]
        public ActionResult SelectPlan(int id, Product product)
        {
            //Consulta de acuerdo al tipo de vendedor la categoria desde la cual va sacar los planes
            int categoryPlanId = _workContext.CurrentVendor.VendorType == Core.Domain.Vendors.VendorType.User ? _planSettings.CategoryProductPlansId : _planSettings.CategoryStorePlansId;
            

            var model = new SelectPlanModel();
            
            string cacheKey = string.Format(ModelCacheEventConsumer.CATEGORY_ACTIVE_PLANS_MODEL_KEY, categoryPlanId);
            model.Plans = _cacheManager.Get(cacheKey, () => { 
            
                //Consulta tdos los productos pertenecientes a la categoria de los planes
                var plans = _productService.SearchProducts(categoryIds: new List<int>() { categoryPlanId });

                var listPlans = new List<SelectPlanModel.PlanModel>();

                foreach (var plan in plans)
                {
                    var planModel = new SelectPlanModel.PlanModel();
                    planModel.Id = plan.Id;
                    //Agrega las caracteristicas del plan
                    foreach (var spec in plan.ProductSpecificationAttributes)
	                {
		                planModel.Specifications.Add(new SelectPlanModel.SpecificationPlan(){
                             Name = spec.SpecificationAttributeOption.SpecificationAttribute.Name,
                             SpecificationAttributeId = spec.SpecificationAttributeOption.SpecificationAttributeId,
                             Value = string.IsNullOrEmpty(spec.CustomValue) ? spec.SpecificationAttributeOption.Name : spec.CustomValue
                        });
	                }

                    listPlans.Add(planModel);
                }

                return listPlans;
            });


            return View(model);
        }

        #region Metodos Privados
        private PublishProductModel GetPublishModel()
        {
            var model = new PublishProductModel();
            model.LimitDaysOfProductPublished = _catalogSettings.LimitDaysOfProductPublished;
            model.StateProvinces = new SelectList(_stateProvinceService.GetStateProvincesByCountryId(_tuilsSettings.defaultCountry), "Id", "Name");
            model.IsMobileDevice = Request.Browser.IsMobileDevice;
            model.HasReachedLimitOfProducts = HasReachedLimitOfProducts();
            model.MaxSizeFileUpload = _tuilsSettings.maxFileUploadSize;
            if (_workContext.CurrentVendor != null)
                model.PhoneNumber = _workContext.CurrentVendor.PhoneNumber;
            return model;
        }
        #endregion
    }

    public enum ProductTypePublished { 
        Product,
        Service,
        Bike
    }
}