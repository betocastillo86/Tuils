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

namespace Nop.Web.Controllers
{
    public class SalesController : Controller
    {

        #region Fields
        private ICategoryService _categoryService;
        private ISpecificationAttributeService _specificationAttributeService;
        private IStateProvinceService _stateProvinceService;
        private TuilsSettings _tuilsSettings;
        private CatalogSettings _catalogSettings;
        private IWorkContext _workContext;
        #endregion

        #region Ctor

        public SalesController(ICategoryService categoryService,
            ISpecificationAttributeService specificationAttributeService,
            IStateProvinceService stateProvinceService,
            TuilsSettings tuilsSettings,
            IWorkContext workContext,
            CatalogSettings catalogSettings)
        {
            this._categoryService = categoryService;
            this._tuilsSettings = tuilsSettings;
            this._specificationAttributeService = specificationAttributeService;
            this._stateProvinceService = stateProvinceService;
            this._workContext = workContext;
            this._catalogSettings = catalogSettings;
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
                model.CanSelectService = _workContext.CurrentVendor.VendorType == Core.Domain.Vendors.VendorType.RepairShop;

            return View(model);
        }

        /// <summary>
        /// Publicación de producto simple
        /// </summary>
        /// <returns></returns>
        public ActionResult PublishProduct()
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
        public ActionResult PublishProductBike()
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
        public ActionResult PublishProductService()
        {
            var model = GetPublishModel();

            //Tipo de producto moto
            model.ProductType = ProductTypePublished.Service;
            model.SubSectionTitle = "Servicios";

            return View("PublishProduct", model);
        }

        #region Metodos Privados
        private PublishProductModel GetPublishModel()
        {
            var model = new PublishProductModel();
            model.LimitDaysOfProductPublished = _catalogSettings.LimitDaysOfProductPublished;
            model.StateProvinces = new SelectList(_stateProvinceService.GetStateProvincesByCountryId(_tuilsSettings.defaultCountry), "Id", "Name");
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