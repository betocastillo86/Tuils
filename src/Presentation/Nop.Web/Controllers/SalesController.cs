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
        private TuilsSettings _tuilsSettings;
        #endregion

        #region Ctor

        public SalesController(ICategoryService categoryService,
            TuilsSettings tuilsSettings)
        {
            this._categoryService = categoryService;
            this._tuilsSettings = tuilsSettings;
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

            //carga las marcas de las fotos validas
            model.BikeBrands = _categoryService.GetCategoryById(this._tuilsSettings.productBaseTypes_bike, true).SubCategories
                .ToList()
                .ToBaseModels();

            return View(model);
        }
	}
}