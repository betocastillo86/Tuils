using Nop.Admin.Models.Preproducts;
using Nop.Services.Catalog;
using Nop.Services.Security;
using Nop.Web.Framework.Kendoui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nop.Admin.Extensions;
using System.Web.Script.Serialization;
using Nop.Services.Customers;
using Nop.Services.Vendors;
using Nop.Core.Domain.Catalog;
using Nop.Core.Infrastructure;
using Nop.Core.Domain.Common;
using Nop.Services.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Vendors;
using Nop.Core;
using Nop.Services.Media;
using Nop.Services.Helpers;

namespace Nop.Admin.Controllers
{
    public class PreproductController : BaseAdminController
    {
        private readonly IPermissionService _permissionService;
        private readonly IPreproductService _preproductService;
        private readonly ICustomerService _customerService;
        private readonly IVendorService _vendorService;
        private readonly ICategoryService _categoryService;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly IProductService _productService;
        private readonly IPictureService _pictureService;
        private readonly IDateTimeHelper _dateTimeHelper;

        #region Constructor
        public PreproductController(IPermissionService permissionService,
            IPreproductService preproductService,
            ICustomerService customerService,
            IVendorService vendorService,
            ICategoryService categoryService,
            IGenericAttributeService genericAttributeService,
            IProductService productService,
            IPictureService pictureService,
            IDateTimeHelper dateTimeHelper)
        {
            _permissionService = permissionService;
            _preproductService = preproductService;
            _customerService = customerService;
            _vendorService = vendorService;
            _categoryService = categoryService;
            _genericAttributeService = genericAttributeService;
            _productService = productService;
            _pictureService = pictureService;
            _dateTimeHelper = dateTimeHelper;
        }
        #endregion

        #region Actions
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageBlog))
                return AccessDeniedView();

            return View();
        }

        [HttpPost]
        public ActionResult List(DataSourceRequest command, PreproductListModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageCategories))
                return AccessDeniedView();

            var preproducts = _preproductService.GetAllPreproducts(model.SearchCustomerEmail, model.SearchProductName,
                command.Page - 1, command.PageSize);
            var gridModel = new DataSourceResult
            {
                Data = preproducts.Select(x =>
                {
                    var preproductModel = x.ToModel();
                    preproductModel.CreatedOn = _dateTimeHelper.ConvertToUserTime(x.CreatedOnUtc, DateTimeKind.Utc).ToString("f");
                    if(x.UpdatedOnUtc.HasValue)
                        preproductModel.UpdatedOn = _dateTimeHelper.ConvertToUserTime(x.UpdatedOnUtc.Value, DateTimeKind.Utc).ToString("f");
                    return preproductModel;
                }),
                Total = preproducts.TotalCount
            };
            return Json(gridModel);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var preproduct = _preproductService.GetById(id);
            return View(preproduct.ToModel());
        }

        [HttpPost]
        [ActionName("Edit")]
        public ActionResult EditPreproduct(int id)
        {
            var preproduct = _preproductService.GetById(id);

            //Carga la información del producto
            var jsonSerializer = new JavaScriptSerializer();
            var model = (ProductBaseModel)jsonSerializer.Deserialize(preproduct.JsonObject, typeof(ProductBaseModel));
            var product = LoadProductFromJson(model);

            //Consulta el cliente
            var customer = _customerService.GetCustomerById(preproduct.CustomerId);

            Vendor vendor = null;
            //Si el vendor no existe, es necesario crearlo con base en el usuario
            if (customer.VendorId == 0)
            {
                //Consulta el vendor y lo crea si es necesario
                vendor = _vendorService.GetVendorByCustomerId(customer.Id, true);
                product.VendorId = vendor.Id;
            }
            else
            {
                product.VendorId = customer.VendorId;
                vendor = _vendorService.GetVendorById(customer.VendorId);
            }

            if (!string.IsNullOrEmpty(model.PhoneNumber))
            {
                //Guarda el número telefónico de contacto
                _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.Phone, model.PhoneNumber);
                vendor.PhoneNumber = model.PhoneNumber;
                _vendorService.UpdateVendor(vendor);
            }

            try
            {
                _productService.PublishProduct(product, vendor, false);
                SuccessNotification("Guardado correctamente");
                RemovePreproductsByCustomer(customer.Id, model.ProductTypeId);
                return RedirectToAction("Edit", "Product", new { id = product.Id });
            }
            catch (Exception e)
            {
                ErrorNotification(e.Message, false);
            }

            return View(preproduct.ToModel());
        }

        private void RemovePreproductsByCustomer(int customerId, int productTypeId)
        {
            var preproducts = _preproductService.GetAllByUserAndType(customerId, productTypeId);

            foreach (var preproduct in preproducts)
            {
                var jsonSerializer = new JavaScriptSerializer();
                var model = (ProductBaseModel)jsonSerializer.Deserialize(preproduct.JsonObject, typeof(ProductBaseModel));
                //Elimina el registro de Base de datos
                _preproductService.Delete(preproduct);

                if (model.TempFiles != null)
                {
                    //Elimina los archivos de base de datos
                    _pictureService.RemovePicturesFromTempFiles(model.TempFiles.ToArray(), 300);
                }
            }
        }

        /// <summary>
        /// CODIGO COPIADO Y PEGADO DE WEB
        /// </summary>
        /// <param name="preproduct"></param>
        /// <returns></returns>
        private Product LoadProductFromJson(ProductBaseModel model)
        {



            var entity = new Product();
            entity.Name = model.Name;
            entity.FullDescription = model.FullDescription;
            entity.ShortDescription = model.FullDescription;
            entity.IsShipEnabled = model.IsShipEnabled;
            entity.AdditionalShippingCharge = model.AdditionalShippingCharge;
            entity.Price = model.Price;
            entity.CallForPrice = model.CallForPrice;

            var _tuilsSettings = EngineContext.Current.Resolve<TuilsSettings>();

            //Agrega la categoria con base en CategoryId
            entity.ProductCategories.Add(new ProductCategory()
            {
                CategoryId = model.CategoryId
            });

            //Agrega las categorias especiales
            if (model.SpecialCategories != null)
            {
                foreach (var specialCategory in model.SpecialCategories)
                {
                    //valida que la categoria exista
                    var category = _categoryService.GetCategoryById(specialCategory.CategoryId);
                    if (category != null)
                    {
                        entity.SpecialCategories.Add(specialCategory);
                        //Si la categoria tiene atributos relacionados (usualmente referencias de motos) la agrega
                        if (category.SpecificationAttributeOptionId.HasValue && entity.ProductSpecificationAttributes.FirstOrDefault(sa => sa.SpecificationAttributeOptionId == category.SpecificationAttributeOptionId.Value) == null)
                        {
                            entity.ProductSpecificationAttributes.Add(new ProductSpecificationAttribute()
                            {
                                SpecificationAttributeOptionId = category.SpecificationAttributeOptionId.Value,
                                AllowFiltering = true
                            });
                        }
                    }
                }
            }

            entity.TempFiles = model.TempFiles;
            entity.IsNew = model.IsNew ?? false;
            entity.StateProvinceId = model.StateProvince;

            #region Properties Products

            if (model.ManufacturerId > 0)
                //Agrega la marca con base en ManufacturerId
                entity.ProductManufacturers.Add(new ProductManufacturer()
                {
                    ManufacturerId = model.ManufacturerId
                });

            #endregion


            #region Properties Bikes

            //Agrega los accesorios
            if (model.Accesories != null && model.Accesories.Count > 0)
                model.Accesories
                    .ForEach(a => entity.ProductSpecificationAttributes
                                            .Add(new ProductSpecificationAttribute()
                                            {
                                                SpecificationAttributeOptionId = a,
                                                ShowOnProductPage = true,
                                                AllowFiltering = true
                                            }
                                            ));
            //Agrega las condiciones de negociación
            if (model.Negotiation != null && model.Negotiation.Count > 0)
                model.Negotiation
                    .ForEach(a => entity.ProductSpecificationAttributes
                                            .Add(new ProductSpecificationAttribute()
                                            {
                                                //SpecificationAttributeOptionId = _tuilsSettings.specificationAttributeNegotiation,
                                                SpecificationAttributeOptionId = a,
                                                ShowOnProductPage = true,
                                                AllowFiltering = true
                                            }
                                            ));

            //Si viene los kilometros los asocia
            if (model.Kms > 0)
                entity.ProductSpecificationAttributes.Add(new ProductSpecificationAttribute() { AttributeType = SpecificationAttributeType.CustomText, CustomValue = model.Kms.ToString(), SpecificationAttributeOptionId = _tuilsSettings.specificationAttributeOptionKms, ShowOnProductPage = true });

            //La placa es exclusiva de las motos
            if (!string.IsNullOrEmpty(model.CarriagePlate))
                entity.ProductSpecificationAttributes.Add(new ProductSpecificationAttribute() { AttributeType = SpecificationAttributeType.CustomText, CustomValue = model.CarriagePlate, SpecificationAttributeOptionId = _tuilsSettings.specificationAttributeOptionCarriagePlate, ShowOnProductPage = false });

            //El año es esclusivo de las motos
            if (model.Year > 0)
                entity.ProductSpecificationAttributes.Add(new ProductSpecificationAttribute() { AttributeType = SpecificationAttributeType.Option, AllowFiltering = true, CustomValue = model.Year.ToString(), SpecificationAttributeOptionId = model.Year, ShowOnProductPage = true });

            if (model.IsNew.HasValue)
                entity.ProductSpecificationAttributes.Add(new ProductSpecificationAttribute() { AttributeType = SpecificationAttributeType.Option, AllowFiltering = true, CustomValue = model.Year.ToString(), SpecificationAttributeOptionId = model.IsNew.Value ? _tuilsSettings.specificationattributeOptionIsNewYes : _tuilsSettings.specificationattributeOptionIsNewNo, ShowOnProductPage = true });


            //En los casos 
            var categoryProduct = _categoryService.GetCategoryById(model.CategoryId);
            if (categoryProduct != null && categoryProduct.SpecificationAttributeOptionId.HasValue)
                entity.ProductSpecificationAttributes.Add(new ProductSpecificationAttribute() { AttributeType = SpecificationAttributeType.Option, AllowFiltering = true, CustomValue = categoryProduct.SpecificationAttributeOptionId.Value.ToString(), SpecificationAttributeOptionId = categoryProduct.SpecificationAttributeOptionId.Value, ShowOnProductPage = true });
            #endregion


            #region Properties Services
            entity.DetailShipping = model.DetailShipping;


            //Agrega los accesorios
            if (model.Supplies != null && model.Supplies.Count > 0)
                model.Supplies
                    .ForEach(s => entity.ProductSpecificationAttributes
                                            .Add(new ProductSpecificationAttribute()
                                            {
                                                SpecificationAttributeOptionId = s,
                                                ShowOnProductPage = true
                                            }
                                           ));
            entity.IncludeSupplies = model.IncludeSupplies;

            if (model.SuppliesValue > 0)
                entity.SuppliesValue = model.SuppliesValue;


            #endregion
            return entity;
        }
        #endregion


    }
}