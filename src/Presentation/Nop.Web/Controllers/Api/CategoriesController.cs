using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Nop.Services.Catalog;
using Nop.Web.Extensions;

namespace Nop.Web.Controllers.Api
{
    [Route("api/categories")]
    public class CategoriesController : ApiController
    {
        #region Fields
        private readonly ICategoryService _categoryService;
        #endregion

        #region Ctors
        public CategoriesController(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }
        #endregion

        

        [HttpGet]
        [Route("api/categories/{id}")]
        public IHttpActionResult Get(int id)
        {
            var category = this._categoryService.GetCategoryById(id, true).ToModel();
            if(category != null)
                return Ok(category);
            else
                return NotFound();
        }
    }
}
