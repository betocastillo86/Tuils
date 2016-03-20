using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Nop.Services.Catalog;
using Nop.Web.Extensions;
using Nop.Web.Extensions.Api;
using Nop.Web.Models.Catalog;
using Nop.Web.Framework.Controllers;
using Nop.Web.Infrastructure.Cache;
using Nop.Core.Caching;
using Nop.Web.Models.Media;
using Nop.Services.Media;
using Nop.Services.Localization;
using Nop.Core.Domain.Media;
using Nop.Services.Seo;


namespace Nop.Web.Controllers.Api
{
    [Route("api/categories")]
    public class CategoriesController : ApiController
    {
        #region Fields
        private readonly ICategoryService _categoryService;
        private readonly IManufacturerService _manufacturerService;
        private readonly ICacheManager _cacheManager;
        private readonly IPictureService _pictureService;
        private readonly ILocalizationService _localizationService;
        private readonly MediaSettings _mediaSettings;
        #endregion

        #region Ctors
        public CategoriesController(ICategoryService categoryService,
            IManufacturerService manufacturerService,
            ICacheManager cacheManager,
            IPictureService pictureService,
            ILocalizationService localizationService,
            MediaSettings mediaSettings)
        {
            this._categoryService = categoryService;
            this._manufacturerService = manufacturerService;
            this._cacheManager = cacheManager;
            this._pictureService = pictureService;
            this._localizationService = localizationService;
            this._mediaSettings = mediaSettings;
        }
        #endregion


        /// <summary>
        /// Retorna la información de una categoria incluyendo las subcategorias en la propiedad ChildrenCategories
        /// </summary>
        /// <param name="id">id de la categoria que se desea filtrar</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/categories/{id:int}")]
        public IHttpActionResult Get(int id)
        {

            //Consulta si debe traer la imagen por medio de los headers
            bool showImage = this.GetHeaderBoolean("image");

            string cacheKey = string.Format(ModelCacheEventConsumer.CATEGORIES_API_CATEGORY_MODEL_KEY, id, showImage);

            var category = _cacheManager.Get(cacheKey, () =>
            {

                var entityCategory = _categoryService.GetCategoryById(id, true, true);
                var model = entityCategory.ToModel();

                //Si debe retornar la imagen la carga en el modelo
                if (showImage)
                {
                    model.PictureModel = entityCategory.GetPicture(_localizationService, _mediaSettings, _pictureService);
                }

                return model;

            });

            if (category != null)
                return Ok(category);
            else
                return NotFound();
        }

        /// <summary>
        /// Retorna todas las marcas dependiendo de la categoria que viene como filtro
        /// </summary>
        /// <param name="id">id de la categoria a filtrar</param>
        /// <returns></returns>
        [Route("api/categories/{id}/manufacturers")]
        public IHttpActionResult GetManufacturesByCategoryId(int id)
        {

            var cacheKey = string.Format(ModelCacheEventConsumer.CATEGORY_MANUFACTURERS_PATTERN_KEY, id);
            var manufacturers = _cacheManager.Get(cacheKey, () => {
                return this._manufacturerService.GetManufacturersByCategoryId(id)
                    .OrderBy(m => m.Name)
                    .ToList()
                    .ToModels();
            }); 

            if (manufacturers != null)
            {
                //si es minificado retorna la lista de los minificados
                if (this.MustMinifyModel())
                    return Ok(manufacturers.ToMinifiedListModel());
                else
                    return Ok(manufacturers);
            }
            else
                return NotFound();
        }


        /// <summary>
        /// Retorna todas las referencias de motos existentes en la App
        /// </summary>
        /// <returns></returns>
        [Route("api/categories/bikereferences")]
        public IHttpActionResult GetAllBikeReferences()
        {
            string cacheKey = ModelCacheEventConsumer.CATEGORIES_API_ALL_BIKEREFERENCES;
            return Ok(_cacheManager.Get(cacheKey, () =>
            {
                var references = this._categoryService.GetAllBikeReferences(null).ToBaseModels();
                return GetMinifiedCategories(references);
            }));
        }

        /// <summary>
        /// Retorna todas las categorias de tipo servicio existentes
        /// </summary>
        /// <returns></returns>
        [Route("api/categories/services")]
        public IHttpActionResult GetAllServices()
        {
            string cacheKey = ModelCacheEventConsumer.CATEGORIES_API_ALL_SERVICES;
            return Ok(_cacheManager.Get(cacheKey, () =>
            {
                var services = this._categoryService.GetAllServices().ToBaseModels();
                return services;
            }));
        }

        [HttpGet]
        [Route("api/categories/{id:int}/subcategories")]
        public IHttpActionResult GetSubcategories(int id)
        {
            string cacheKey = string.Format(ModelCacheEventConsumer.CATEGORIES_API_CATEGORY_MODEL_KEY, id, false);

            var category = _cacheManager.Get(cacheKey, () =>
            {
                var entityCategory = _categoryService.GetCategoryById(id, true, true);
                return entityCategory.ToModel();
            });

            if (category != null)
                return Ok(category.ChildrenCategories);
            else
                return NotFound();
        }

        private List<object> GetMinifiedCategories(List<CategoryBaseModel> list)
        {
            //Ya que son muchas referencias se retorna un objeto con el minimo de información posible
            var minlist = new List<object>();

            foreach (var reference in list)
            {
                minlist.Add(new
                    {
                        Id = reference.Id,
                        Name = reference.Name,
                        ChildrenCategories = (reference.ChildrenCategories != null && reference.ChildrenCategories.Count > 0) ? reference.ChildrenCategories.Select(r => new { Id = r.Id, Name = r.Name }) : null
                    });
            }
            return minlist;
        }
    }
}
