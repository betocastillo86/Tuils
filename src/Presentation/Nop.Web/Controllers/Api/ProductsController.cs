﻿using Nop.Web.Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Nop.Web.Extensions.Api;
using Nop.Services.Catalog;
using Nop.Core;
using Nop.Services.Vendors;
using Nop.Web.Framework.Mvc.Api;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Services.Common;
using Nop.Core.Domain.Customers;
using Nop.Services.Localization;
using Nop.Core.Domain.Media;
using Nop.Services.Media;
using Nop.Web.Models.Media;
using System.Threading.Tasks;
using Nop.Web.Infrastructure;
using Nop.Services.Seo;
using Nop.Utilities;
using Nop.Web.Models.Catalog;
using Nop.Services.Logging;
using Nop.Core.Infrastructure;


namespace Nop.Web.Controllers.Api
{
    [Route("api/products")]
    public class ProductsController : ApiController
    {
        #region Fields
        private readonly IProductService _productService;
        private readonly IPreproductService _preproductService;
        private readonly IWorkContext _workContext;
        private readonly IVendorService _vendorService;
        private readonly ICategoryService _categoryService;
        private readonly CatalogSettings _catalogSettings;
        private readonly PlanSettings _planSettings;
        private readonly TuilsSettings _tuilsSettings;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly ILocalizationService _localizationService;
        private readonly MediaSettings _mediaSettings;
        private readonly IPictureService _pictureService;
        private readonly IPriceFormatter _priceFormatter;
        private readonly ILogger _logger;
        #endregion

        #region Ctor
        public ProductsController(IProductService productService, 
            IWorkContext workContext,
            IVendorService vendorService,
            ICategoryService categoryService,
            CatalogSettings catalogSettings,
            TuilsSettings tuilsSettings,
            IGenericAttributeService genericAttributeService,
            ILocalizationService localizationService,
            MediaSettings mediaSettings,
            IPictureService pictureService,
            IPriceFormatter priceFormatter,
            PlanSettings planSettings,
            ILogger logger,
            IPreproductService preproductService)
        {
            this._productService = productService;
            this._workContext = workContext;
            this._vendorService = vendorService;
            this._categoryService = categoryService;
            this._catalogSettings = catalogSettings;
            this._tuilsSettings = tuilsSettings;
            this._genericAttributeService = genericAttributeService;
            this._localizationService = localizationService;
            this._mediaSettings = mediaSettings;
            this._pictureService = pictureService;
            this._priceFormatter = priceFormatter;
            this._planSettings = planSettings;
            this._logger = logger;
            this._preproductService = preproductService;
        }
        #endregion
        [Route("api/products")]
        [HttpPost]
        [AuthorizeApi]
        public IHttpActionResult PublishProduct(ProductBaseModel model)
        {
            if (ModelState.IsValid && model.Validate(ModelState))
            {

                try
                {
                    //Si llegan más valores de los posibles para categorias especiales, toma solo los permitidos
                    if (model.SpecialCategories != null && model.SpecialCategories.Where(s => s.SpecialType == SpecialCategoryProductType.BikeBrand).Count() > _catalogSettings.LimitOfSpecialCategories)
                        model.SpecialCategories = model.SpecialCategories.Take(_catalogSettings.LimitOfSpecialCategories).ToList();


                    var product = model.ToEntity(_categoryService);

                    //Si el vendor no existe, es necesario crearlo con base en el usuario
                    if (_workContext.CurrentVendor == null || _workContext.CurrentVendor.Id == 0)
                    {
                        //Consulta el vendor y lo crea si es necesario
                        var vendor = _vendorService.GetVendorByCustomerId(_workContext.CurrentCustomer.Id, true);
                        product.VendorId = vendor.Id;
                    }
                    else
                    {
                        product.VendorId = _workContext.CurrentVendor.Id;
                        //int limitProduts;
                        //if(_productService.HasReachedLimitOfProducts(_workContext.CurrentVendor, out limitProduts))
                        //    throw new NopException(CodeNopException.UserHasReachedLimitOfProducts, _localizationService.GetResource("PublishProduct.HasReachedLimitOfProducts", limitProduts));
                    }

                    //Guarda el número telefónico de contacto
                    _genericAttributeService.SaveAttribute(_workContext.CurrentCustomer, SystemCustomerAttributeNames.Phone, model.PhoneNumber);
                    _workContext.CurrentVendor.PhoneNumber = model.PhoneNumber;
                    _vendorService.UpdateVendor(_workContext.CurrentVendor);

                    //Si se debe validar si el producto es repetido y el usuario tiene un producto similar publicado
                    //retorna el error
                    if (!model.OmitRepetedProduct && _productService.UserHasSimilarProductPublised(product, 1))
                    {
                        _logger.Warning(string.Format("Usuario intenta publicar un producto doble vez: {0}", product.Name));
                        ModelState.AddModelError("ErrorCode", Convert.ToInt32(CodeNopException.UserHasHasPublishedSimilarProduct).ToString());
                        ModelState.AddModelError("ErrorMessage", _localizationService.GetResource("PublishProduct.AskUserPublishSimilarProduct"));
                        return BadRequest(ModelState);
                    }


                    //Crea el producto en un estado inactivo 
                    _productService.PublishProduct(product);

                    //Elimina los preproductos existentes previamente
                    Task.Factory.StartNew(() => {
                        var preproductService = EngineContext.Current.Resolve<IPreproductService>();
                        var pictureService = EngineContext.Current.Resolve<IPictureService>();

                        var preproducts = preproductService.GetAllByUserAndType(_workContext.CurrentCustomer.Id, model.ProductTypeId);
                        if (preproducts.Count > 0)
                        {
                            foreach (var preproduct in preproducts)
                            {
                                var modelPreproduct = preproduct.ToSerializedObject();
                                //Elimina el registro de Base de datos
                                preproductService.Delete(preproduct);

                                if (modelPreproduct.TempFiles != null)
                                {
                                    //Elimina los archivos de base de datos
                                    pictureService.RemovePicturesFromTempFiles(modelPreproduct.TempFiles.ToArray(), 300);
                                }
                            }
                        }
                    });

                    //_preproductService.RemovePreproductsByCustomerId(_workContext.CurrentCustomer.Id, model.ProductTypeId);

                    return Ok(new { Id = product.Id });
                }
                catch (NopException e)
                {
                    _logger.Error("Excepción publicando el producto", e);
                    ModelState.AddModelError("ErrorCode", Convert.ToInt32(e.Code).ToString());
                    ModelState.AddModelError("ErrorMessage", e.Message);
                    return BadRequest(ModelState);
                }
                /*catch (Exception e)
                {
                    _logger.Error("Excepción publicando el producto", e);
                    return InternalServerError(new Exception("Ocurrió un error guardando el producto"));
                }*/

            }
            else
            {
                //var errors = new System.Text.StringBuilder();
                //foreach (var error in ModelState.Values)
                //{
                //    errors.AppendFormat("{0}\n", error);
                //}
                _logger.Error("Contenido del model State" + ModelState.ToErrorString());
                return BadRequest(ModelState);
            }
        }


        [Route("api/products")]
        [HttpPut]
        [AuthorizeApi]
        public IHttpActionResult Update(EditProductModel model)
        {
            if (ModelState.IsValid)
            {
                var product = _productService.GetProductById(model.Id);

                if (product == null || product.VendorId != _workContext.CurrentVendor.Id)
                    return Unauthorized();


                //Actualiza el modelo
                product.Name = model.Name;
                product.ShortDescription = model.ShortDescription;
                product.FullDescription = model.ShortDescription;
                product.Price = model.Price;

                _productService.UpdateProduct(product);

                return Ok(model);
            }
            else {
                return BadRequest(ModelState);
            }

        }


        [Route("api/products/{id}/enable")]
        [HttpPut]
        [AuthorizeApi]
        public IHttpActionResult Enable(int id)
        {
            if (_workContext.CurrentVendor == null)
                return Unauthorized();

            var product = _productService.GetProductById(id);

            if (product != null)
            {
                if (product.VendorId == _workContext.CurrentVendor.Id)
                {
                    try
                    {
                        _productService.EnableProduct(product);
                        return Ok(product.ToModel(_priceFormatter, _localizationService, _mediaSettings, _pictureService));
                    }
                    catch (NopException e)
                    {
                        //ModelState.AddModelError("ErrorEnabling", e.ToString());
                        return BadRequest(e.Message);
                    }
                }
                else
                {
                    return Unauthorized();
                }
            }
            else
            {
                return NotFound();
            }
        }

        [Route("api/products/{id}")]
        [HttpDelete]
        [AuthorizeApi]
        public IHttpActionResult Delete(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null || product.VendorId != _workContext.CurrentVendor.Id)
                return Unauthorized();

            //Se desactia el producto
            product.Sold = true;
            _productService.UpdateProduct(product);

            return Ok(new { deleted = true, Id = id });
        }

        #region Pictures
        [Route("api/products/{id}/pictures")]
        [HttpGet]
        [AuthorizeApi]
        public IHttpActionResult GetPictures(int id)
        {
            var pictures = _pictureService.GetPicturesByProductId(id);
            var pictureModels = new List<PictureOrderedModel>();
            int displayOrder = 0;
            foreach (var picture in pictures)
            {
                pictureModels.Add(new PictureOrderedModel
                {
                    Id = picture.Id,
                    ImageUrl = _pictureService.GetPictureUrl(picture, _mediaSettings.ProductThumbPictureSizeOnProductDetailsPage, crop: true),
                    FullSizeImageUrl = _pictureService.GetPictureUrl(picture),
                    DisplayOrder = displayOrder++
                });
            }

            return Ok(pictureModels);
        }

        [Route("api/products/{productId}/pictures/{pictureId}")]
        [HttpDelete]
        [AuthorizeApi]
        public IHttpActionResult DeleteProductPicture(int productId, int pictureId)
        {

            var product = _productService.GetProductById(productId);
            
            //El producto debe pertenecer al mismo vendedor de la sesión
            if(product == null || product.VendorId != _workContext.CurrentVendor.Id)
                return NotFound();
            
            //Minimo puede quedar con una imagen en el producto
            if (product.ProductPictures.Count <= 1)
                return BadRequest("No se pueden eliminar todas las imagenes");
            
            var picture = product.ProductPictures.FirstOrDefault(p => p.PictureId == pictureId);
            if (picture == null)
                return NotFound();

            //Si la imagen que desea eliminar es la por defecto para servicios, solo elimina la relacion
            if (picture.PictureId != _catalogSettings.DefaultServicePicture)
                _pictureService.DeletePicture(picture.Picture);
            else
                _productService.DeleteProductPicture(picture);
            

            return Ok(new { deleted = true });
        }

        [Route("api/products/{productId}/pictures/{pictureId?}")]
        [HttpPost]
        [AuthorizeApi]
        [AsyncFileUploadApiFilter]
        public IHttpActionResult SavePicture(int productId, int? pictureId = null)
        {
            if (productId > 0 &&
               Request.Properties["fileUploaded"] != null &&
               _workContext.CurrentVendor != null)
            {

                var fileToUpload = Request.Properties["fileUploaded"] as FileData;

                var product = _productService.GetProductById(productId);

                if (product == null || product.VendorId != _workContext.CurrentVendor.Id)
                    return NotFound();


                Picture picture = null;
                //Si viene id de la foto actualiza los datos, sino inserta una nueva
                if (pictureId.HasValue)
                {
                    if (pictureId.Value == _catalogSettings.DefaultServicePicture)
                        return BadRequest("No se puede modificar la imagen de servicios por defecto. Hay que eliminarla");
                    
                    //Valida que la foto exista
                    var prodPicture = product.ProductPictures.FirstOrDefault(p => p.PictureId == pictureId);
                    if (prodPicture == null)
                        return NotFound();

                    picture = prodPicture.Picture;
                    _pictureService.UpdatePicture(pictureId.Value, fileToUpload.Data, fileToUpload.ContentType, product.GetSeName(), true);
                }
                else
                {
                    //Compara las imagenes del plan contra las imagenes activas del usuario
                    bool activeImage = product.ProductPictures.Where(p => p.Active).Count() < _workContext.CurrentVendor.GetCurrentPlan(_productService, _planSettings).NumPictures;
                    
                    //Inserta la imagen y la asocia
                    picture = _productService.InsertProductPicture(productId, fileToUpload.Data, fileToUpload.ContentType, product.GetSeName(), true, displayOrder: product.ProductPictures.Count, active:activeImage).Picture;
                }

                //Retorna el modelo de la imagen
                return Ok(new PictureOrderedModel()
                {
                    Id = picture.Id,
                    ImageUrl = _pictureService.GetPictureUrl(picture, _mediaSettings.ProductThumbPictureSizeOnProductDetailsPage, crop: true),
                    FullSizeImageUrl = _pictureService.GetPictureUrl(picture),
                });
            }
            else
                return BadRequest();
        }


        /// <summary>
        /// Permite ver más información del producto, suma la vista
        /// </summary>
        /// <param name="id">id del producto que se desea ver más info</param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/products/{productId}/moreinfo")]
        public IHttpActionResult SeeMoreInfo(int productId)
        {
            if (productId <= 0)
                return NotFound();

            //Consulta y valida que el producto exista
            var product = _productService.GetProductById(productId);
            if (product == null)
                return NotFound();

            //Suma la visita
            product.NumClicksForMoreInfo++;
            _productService.UpdateProduct(product);

            return Ok( new { Id = product.Id });
        }

        #endregion

        #region ProductsByVendor
        [HttpGet]
        [Route("api/vendors/{id}/products", Order = 1)]
        public IHttpActionResult GetProductsByVendor(int id, [FromUri]FilterProductsModel filter)
        {
            if (id <= 0)
                return NotFound();

            var products = _productService.SearchProducts(vendorId:id);


            IQueryable<Product> query = products.AsQueryable();

            if (filter.OnHome.HasValue && filter.OnHome.Value)
                query = query.Where(p => p.ShowOnHomePage);

            if(filter.OnSliders.HasValue && filter.OnSliders.Value)
                query = query.Where(p => p.FeaturedForSliders);

            if (filter.OnSN.HasValue && filter.OnSN.Value)
                query = query.Where(p => p.SocialNetworkFeatured);

            return Ok(query.ToList().ToModels(_priceFormatter, 
                _localizationService, 
                _mediaSettings, 
                _pictureService));
        }
        #endregion
    }
}