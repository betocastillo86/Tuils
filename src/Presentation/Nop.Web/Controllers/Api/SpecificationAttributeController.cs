using Nop.Core.Domain.Common;
using Nop.Services.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Nop.Web.Extensions.Api;

namespace Nop.Web.Controllers.Api
{
    [Route("api/attributes")]
    public class SpecificationAttributeController : ApiController
    {
        #region Attributes
        private readonly TuilsSettings _tuilsSettings;
        private readonly ISpecificationAttributeService _specificationAttributeService;
        #endregion

        #region Ctor
        public SpecificationAttributeController(TuilsSettings tuilsSettings,
            ISpecificationAttributeService specificationAttributeService
            )
        {
            this._tuilsSettings = tuilsSettings;
            this._specificationAttributeService = specificationAttributeService;
        }
        #endregion

        #region Metodos Publicos
        /// <summary>
        /// Retorna los datos y las opciones de un atributo
        /// </summary>
        /// <param name="id">id del attributo</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/attributes")]
        public IHttpActionResult Get(int id)
        {
            if (id > 0)
            {
                var attribute = _specificationAttributeService.GetSpecificationAttributeById(id);
                if (attribute != null)
                    return Ok(attribute.ToModel());
                else
                    return NotFound();
            }
            else
            {
                return NotFound();
            }

        }

        /// <summary>
        /// Retorna los insumos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/attributes/supplies")]
        public IHttpActionResult GetSupplies()
        {
            return Get(_tuilsSettings.specificationAttributeSupplies);
        }
        #endregion



    }
}
