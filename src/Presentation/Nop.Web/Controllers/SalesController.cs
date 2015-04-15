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

namespace Nop.Web.Controllers
{
    public class SalesController : Controller
    {

        #region Fields
        private ICategoryService _categoryService;
        private ISpecificationAttributeService _specificationAttributeService;
        private IStateProvinceService _stateProvinceService;
        private TuilsSettings _tuilsSettings;
        #endregion

        #region Ctor

        public SalesController(ICategoryService categoryService,
            ISpecificationAttributeService specificationAttributeService,
            IStateProvinceService stateProvinceService,
            TuilsSettings tuilsSettings)
        {
            this._categoryService = categoryService;
            this._tuilsSettings = tuilsSettings;
            this._specificationAttributeService = specificationAttributeService;
            this._stateProvinceService = stateProvinceService;
        }
        #endregion

        // GET: /quiero-vender/
        /// <summary>
        /// Pantalla principal para vender un producto
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
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

            return View("PublishProduct", model);
        }

        #region Metodos Privados
        private PublishProductModel GetPublishModel()
        {
            var model = new PublishProductModel();
            model.StateProvinces = new SelectList(_stateProvinceService.GetStateProvincesByCountryId(_tuilsSettings.defaultCountry), "Id", "Name");
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