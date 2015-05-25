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


namespace Nop.Web.Controllers.Api
{
    [Route("api/categories")]
    public class CategoriesController : ApiController
    {
        #region Fields
        private readonly ICategoryService _categoryService;
        private readonly IManufacturerService _manufacturerService;
        private readonly ICacheManager _cacheManager;
        #endregion

        #region Ctors
        public CategoriesController(ICategoryService categoryService,
            IManufacturerService manufacturerService,
            ICacheManager cacheManager)
        {
            this._categoryService = categoryService;
            this._manufacturerService = manufacturerService;
            this._cacheManager = cacheManager;
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
            string cacheKey = string.Format(ModelCacheEventConsumer.CATEGORIES_API_CATEGORY_MODEL_KEY, id);

            var category = _cacheManager.Get(cacheKey, () => {
                return _categoryService.GetCategoryById(id, true).ToModel();
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
            var manufacturers = this._manufacturerService.GetManufacturersByCategoryId(id).ToModels();

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
            var services = this._categoryService.GetAllServices().ToBaseModels();

            if (services != null)
                return Ok(services);
            else
                return NotFound();
        }

        private List<object> GetMinifiedCategories(List<CategoryBaseModel> list)
        {
            //Ya que son muchas referencias se retorna un objeto con el minimo de información posible
            var minlist = new List<object>();

            foreach (var reference in list)
            {
                minlist.Add(
                    new
                    {
                        Id = reference.Id,
                        Name = reference.Name,
                        ChildrenCategories = reference.ChildrenCategories.Select(r => new { Id = r.Id, Name = r.Name })
                    });

            }
            return minlist;
        }
    }
}
