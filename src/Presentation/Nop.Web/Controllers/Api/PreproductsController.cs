using Nop.Core;
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
using Nop.Web.Extensions.Api;
using Nop.Services.Media;

namespace Nop.Web.Controllers.Api
{
    [Route("api/preproducts")]
    public class PreproductsController : ApiController
    {

        #region Fields
        private readonly IPreproductService _preproductService;
        private readonly IPictureService _pictureService;
        private readonly IWorkContext _workContext;

        #endregion
        public PreproductsController(IPreproductService preproductService,
            IWorkContext workContext,
            IPictureService pictureService)
        {
            _preproductService = preproductService;
            _workContext = workContext;
            _pictureService = pictureService;
        }

        [HttpGet]
        [AuthorizeApi]
        public IHttpActionResult GetByType(int productType)
        {
            var preproduct = _preproductService.GetByUserAndType(_workContext.CurrentCustomer.Id, productType);
            
            //Si existe preproducto lo retorna, sino retorna 0
            if (preproduct != null)
            {
                
                //var model = (ProductBaseModel)jsonSerializer.DeserializeObject(preproduct.JsonObject);
                var model = preproduct.ToSerializedObject();
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

        /// <summary>
        /// Elimina los preproductos de un usuario
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [AuthorizeApi]
        [Route("api/preproducts")]
        public IHttpActionResult Delete(int productType)
        {
            var preproducts = _preproductService.GetAllByUserAndType(_workContext.CurrentCustomer.Id, productType);
            if (preproducts.Count > 0)
            {
                foreach (var preproduct in preproducts)
                {
                    var model = preproduct.ToSerializedObject();
                    //Elimina el registro de Base de datos
                    _preproductService.Delete(preproduct);

                    if (model.TempFiles != null)
                    {
                        //Elimina los archivos de base de datos
                        _pictureService.RemovePicturesFromTempFiles(model.TempFiles.ToArray(), 300);
                    }
                }

                return Ok(new { Id = 0 });
            }
            else
            {
                return NotFound();
            }
        }

        private void Save(ProductBaseModel model)
        {
            //Guarda los dats del preproducto
            var preproduct = new Preproduct();
            preproduct.CustomerId = _workContext.CurrentCustomer.Id;
            preproduct.JsonObject = new JavaScriptSerializer().Serialize(model);
            preproduct.ProductTypeId = model.ProductTypeId;
            preproduct.ProductName = model.Name;
            
            if (Request.Headers.UserAgent != null)
                preproduct.UserAgent = Request.Headers.UserAgent.ToString();

            preproduct.Id = model.Id;
            //Actualiza los datos y retorna los valores
            _preproductService.SavePreproduct(preproduct);
            //Retorna la respuesta
            model.Id = preproduct.Id;
        }
    }
}