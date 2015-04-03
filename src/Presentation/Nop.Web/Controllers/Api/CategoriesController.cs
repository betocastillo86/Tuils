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


namespace Nop.Web.Controllers.Api
{
    [Route("api/categories")]
    public class CategoriesController : ApiController
    {
        #region Fields
        private readonly ICategoryService _categoryService;
        private readonly IManufacturerService _manufacturerService;
        #endregion

        #region Ctors
        public CategoriesController(ICategoryService categoryService,
            IManufacturerService manufacturerService)
        {
            this._categoryService = categoryService;
            this._manufacturerService = manufacturerService;
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
            var category = this._categoryService.GetCategoryById(id, true).ToModel();
            if(category != null)
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
            var references = this._categoryService.GetAllBikeReferences(null).ToBaseModels();

            //Ya que son muchas referencias se retorna un objeto con el minimo de información posible
            var minReferences = new List<object>();

            foreach (var reference in references)
	        {
                minReferences.Add(
                    new { 
                        Id = reference.Id, 
                        Name = reference.Name,
                        ChildrenCategories = reference.ChildrenCategories.Select(r => new { Id = r.Id, Name = r.Name })
                    });
                
	        }

            if (references != null)
                return Ok(minReferences);
            else
                return NotFound();
        }
    }
}
