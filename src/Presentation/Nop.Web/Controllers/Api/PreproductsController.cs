﻿using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Services.Catalog;
using Nop.Web.Framework.Mvc.Api;
using Nop.Web.Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace Nop.Web.Controllers.Api
{
    [Route("api/preproducts")]
    public class PreproductsController : ApiController
    {

        #region Fields
        private readonly IPreproductService _preproductService;
        private readonly IWorkContext _workContext;
        #endregion
        public PreproductsController(IPreproductService preproductService,
            IWorkContext workContext)
        {
            _preproductService = preproductService;
            _workContext = workContext;
        }

        [HttpGet]
        [AuthorizeApi]
        public IHttpActionResult GetByType(int productType)
        {
            var preproduct = _preproductService.GetByUserAndType(_workContext.CurrentCustomer.Id, productType);
            
            //Si existe preproducto lo retorna, sino retorna 0
            if (preproduct != null)
            {
                var jsonSerializer = new JavaScriptSerializer();
                //var model = (ProductBaseModel)jsonSerializer.DeserializeObject(preproduct.JsonObject);
                var model = (ProductBaseModel)jsonSerializer.Deserialize(preproduct.JsonObject, typeof(ProductBaseModel));
                model.Id = preproduct.Id;
                return Ok(model);
            }
            else
            {
                return Ok(new { Id = 0});
            }
        }

        [HttpPost]
        [AuthorizeApi]
        public IHttpActionResult Post(ProductBaseModel model)
        {
            Save(model);
            return Ok(new { Id = model.Id });
        }

        [HttpPut]
        [AuthorizeApi]
        public IHttpActionResult Put(ProductBaseModel model)
        {
            Save(model);
            return Ok(new { Id = model.Id });
        }

        private void Save(ProductBaseModel model)
        {
            //Guarda los dats del preproducto
            var preproduct = new Preproduct();
            preproduct.CustomerId = _workContext.CurrentCustomer.Id;
            preproduct.JsonObject = new JavaScriptSerializer().Serialize(model);
            preproduct.ProductTypeId = model.ProductTypeId;
            preproduct.Id = model.Id;
            //Actualiza los datos y retorna los valores
            _preproductService.SavePreproduct(preproduct);
            //Retorna la respuesta
            model.Id = preproduct.Id;
        }
    }
}