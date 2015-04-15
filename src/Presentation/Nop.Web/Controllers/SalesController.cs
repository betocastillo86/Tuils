using Nop.Core.Domain.Common;
using Nop.Services.Catalog;
using Nop.Web.Models.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nop.Web.Extensions;

namespace Nop.Web.Controllers
{
    public class SalesController : Controller
    {

        #region Fields
        private ICategoryService _categoryService;
        private ISpecificationAttributeService _specificationAttributeService;
        private TuilsSettings _tuilsSettings;
        #endregion

        #region Ctor

        public SalesController(ICategoryService categoryService,
            ISpecificationAttributeService specificationAttributeService,
            TuilsSettings tuilsSettings)
        {
            this._categoryService = categoryService;
            this._tuilsSettings = tuilsSettings;
            this._specificationAttributeService = specificationAttributeService;
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

        public ActionResult PublishProduct()
        {
            var model = new PublishProductModel();

            //Tipo de producto servicio
            model.ProductType = ProductTypePublished.Product;

            return View(model);
        }

        public ActionResult PublishProductBike()
        {
            var model = new PublishProductModel();
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
	}

    public enum ProductTypePublished { 
        Product,
        Service,
        Bike
    }
}