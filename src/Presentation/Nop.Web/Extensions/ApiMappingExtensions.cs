using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Media;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Vendors;
using Nop.Core.Infrastructure;
using Nop.Web.Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nop.Services.Seo;
using Nop.Services.Helpers;
using Nop.Services.Directory;
using Nop.Services.Catalog;

namespace Nop.Web.Extensions.Api
{
    public static class ApiMappingExtensions
    {
        #region ProductBaseModel
        public static Product ToEntity(this ProductBaseModel model, ICategoryService categoryService)
        {
            var entity = new Product();
            entity.Name = model.Name;
            entity.FullDescription = model.FullDescription;
            entity.IsShipEnabled = model.IsShipEnabled;
            //entity.AdditionalShippingCharge = model.AdditionalShippingCharge;
            entity.Price = model.Price;

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
                    var category = categoryService.GetCategoryById(specialCategory.CategoryId);
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
            entity.IsNew = model.IsNew;
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
                                                ShowOnProductPage = true
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
                                                ShowOnProductPage = true
                                            }
                                            ));

            //Si viene los kilometros los asocia
            if (model.Kms > 0)
                entity.ProductSpecificationAttributes.Add(new ProductSpecificationAttribute() { AttributeType = SpecificationAttributeType.CustomText, CustomValue = model.Kms.ToString(), SpecificationAttributeOptionId = _tuilsSettings.specificationAttributeOptionKms, ShowOnProductPage = true });

            //La placa es exclusiva de las motos
            if (!string.IsNullOrEmpty(model.CarriagePlate))
                entity.ProductSpecificationAttributes.Add(new ProductSpecificationAttribute() { AttributeType = SpecificationAttributeType.CustomText, CustomValue = model.Kms.ToString(), SpecificationAttributeOptionId = _tuilsSettings.specificationAttributeOptionCarriagePlate, ShowOnProductPage = true });

            //El año es esclusivo de las motos
            if (model.Year > 0)
                entity.ProductSpecificationAttributes.Add(new ProductSpecificationAttribute() { AttributeType = SpecificationAttributeType.Option, AllowFiltering = true, CustomValue = model.Year.ToString(), SpecificationAttributeOptionId = model.Year, ShowOnProductPage=true });

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

        #region SpecificationAttribute
        public static SpecificationAttributeModel ToModel(this SpecificationAttribute entity)
        {
            var model = new SpecificationAttributeModel();
            model.Id = entity.Id;
            model.Name = entity.Name;
            model.Options = entity.SpecificationAttributeOptions.ToList().ToMinifiedListModel();
            return model;
        }
        #endregion

        #region Customer
        public static Customer ToEntity(this CustomerBaseModel model, out Dictionary<string, object> attributes)
        {
            var entity = new Customer();
            entity.Email = model.Email;
            entity.Password = model.Password;
            attributes = new Dictionary<string, object>();
            //Agrega los atributos básicos del registro
            attributes.Add(SystemCustomerAttributeNames.FirstName, model.Name);
            attributes.Add(SystemCustomerAttributeNames.LastName, model.LastName);

            if (model.VendorType != Core.Domain.Vendors.VendorType.User)
                attributes.Add(SystemCustomerAttributeNames.Company, model.CompanyName);

            return entity;
        }

        #endregion

        #region Address

        public static List<AddressModel> ToModels(this IList<Address> list)
        {
            var models = new List<AddressModel>();
            foreach (var entity in list)
            {
                models.Add(entity.ToModel());
            }
            return models;
        }

        public static AddressModel ToModel(this Address entity)
        {
            return new AddressModel()
            {
                Id = entity.Id,
                Address = entity.Address1,
                Email = entity.Email,
                PhoneNumber = entity.PhoneNumber,
                FaxNumber = entity.FaxNumber,
                StateProvinceId = entity.StateProvinceId.Value,
                DisplayOrder = entity.DisplayOrder,
                Schedule = entity.Schedule,
                VendorId = entity.VendorId ?? 0,
                Name = entity.FirstName,
                Latitude = entity.Latitude ?? 0,
                Longitude = entity.Longitude ?? 0,
                StateProvinceName = entity.StateProvince != null ? entity.StateProvince.Name : null
            };
        }

        public static Address ToEntity(this AddressModel model)
        {
            return new Address()
            {
                Id = model.Id,
                Address1 = model.Address,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                FaxNumber = model.FaxNumber,
                StateProvinceId = model.StateProvinceId,
                DisplayOrder = model.DisplayOrder,
                Schedule = model.Schedule,
                VendorId = model.VendorId,
                FirstName = model.Name,
                Latitude = model.Latitude,
                Longitude = model.Longitude
            };
        }

        #region AddressPictureModel
        public static Nop.Web.Models.Media.PictureModel ToModel(this Picture entity, string name, int size, Nop.Services.Media.IPictureService _pictureService, Nop.Services.Localization.ILocalizationService _localizationService)
        {
            var model = new AddressPictureModel
            {
                ImageUrl = _pictureService.GetPictureUrl(entity, size),
                FullSizeImageUrl = _pictureService.GetPictureUrl(entity),
                Title = string.Format(_localizationService.GetResource("Media.Product.ImageLinkTitleFormat"), name),
                AlternateText = string.Format(_localizationService.GetResource("Media.Product.ImageAlternateTextFormat"), name),
                Id = entity.Id
            };
            return model;
        }

        public static List<Nop.Web.Models.Media.PictureModel> ToModels(this IList<Picture> list, string name, int size, Nop.Services.Media.IPictureService _pictureService, Nop.Services.Localization.ILocalizationService _localizationService)
        {
            var models = new List<Nop.Web.Models.Media.PictureModel>();
            foreach (var entity in list)
            {
                models.Add(entity.ToModel(name, size, _pictureService, _localizationService));
            }
            return models;

        }
        #endregion

        #endregion

        #region Vendor
        public static Vendor ToEntity(this VendorModel model)
        {
            return new Vendor()
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                EnableShipping = model.EnableShipping,
                EnableCreditCardPayment = model.EnableCreditCardPayment
            };
        }
        #endregion

        #region Order

        //public static OrderItemModel ToModel(this Order entity)
        //{
        //    var orderModel = new OrderItemModel
        //    {
        //        Id = entity.Id,
        //        CreatedOn = EngineContext.Current.Resolve<IDateTimeHelper>().ConvertToUserTime(entity.CreatedOnUtc, DateTimeKind.Utc)
        //    };

        //    var orderTotalInCustomerCurrency = EngineContext.Current.Resolve<ICurrencyService>().ConvertCurrency(entity.OrderTotal, entity.CurrencyRate);
        //    orderModel.Price = EngineContext.Current.Resolve<IPriceFormatter>().FormatPrice(orderTotalInCustomerCurrency, true, entity.CustomerCurrencyCode, false, _workContext.WorkingLanguage);

        //    if (entity.OrderItems.Count > 0)
        //    {
        //        var item = entity.OrderItems.FirstOrDefault();
        //        orderModel.Rating = item.Rating;
        //        orderModel.Product = new ProductBaseModel()
        //        {
        //            Id = item.Product.Id,
        //            Name = item.Product.Name,
        //            Link = item.Product.GetSeName()
        //        };

        //        orderModel.Vendor = new VendorModel()
        //        {
        //            Id = item.Product.VendorId,
        //            Name = item.Product.Vendor.Name,
        //            Link = Action. item.Product.Vendor.GetSeName()
        //        };
        //    }
        //    else
        //    {
        //        throw new NopException("La orden no tiene productos asociados");
        //    }
        //}

        //public static List<OrderItemModel> ToModels(this List<Order> orders)
        //{
        //    foreach (var order in orders)
        //    {


        //        model.Orders.Add(orderModel);
        //    }
        //}

        #endregion

        #region ProductReviewModel

        public static ProductReviewModel ToModel(this ProductReview entity, System.Web.Http.Routing.UrlHelper urlHelper = null)
        {
            return new ProductReviewModel()
            {
                CustomerId = entity.CustomerId,
                //CustomerName = entity.Customer != null ? entity.Customer.attr
                IsApproved = entity.IsApproved,
                ProductId = entity.ProductId,
                ProductName = entity.Product != null ? entity.Product.Name : null,
                ProductUrl = urlHelper != null ? urlHelper.Route("Product", new { action = "ProductDetails", controller = "Product", SeName = entity.Product.GetSeName()} ) : string.Empty,
                Rating = entity.Rating,
                ReviewText = entity.ReviewText,
                Title = entity.Title,
                CreatedOnUtcTicks = entity.CreatedOnUtc.Ticks,
                CreatedOnUtc = entity.CreatedOnUtc,
                CreatedOnUtcStr = entity.CreatedOnUtc.ToString("g")
            };

        }


        public static List<ProductReviewModel> ToModels(this IList<ProductReview> entities, System.Web.Http.Routing.UrlHelper urlHelper = null)
        {
            var models = new List<ProductReviewModel>();

            foreach (var entity in entities)
            {
                models.Add(entity.ToModel(urlHelper));
            }

            return models;
        }
        #endregion

        #region Questions
        public static ProductQuestionModel ToModel(this ProductQuestion entity, IDateTimeHelper _dateTimeHelper)
        {
            var model = new ProductQuestionModel()
            {
                Id = entity.Id,
                CustomerId = entity.CustomerId,
                ProductId = entity.ProductId,
                QuestionText = entity.QuestionText,
                CreatedOnUtc = entity.CreatedOnUtc,
                AnsweredOnUtc = entity.AnsweredOnUtc,
                CreatedOnStr = _dateTimeHelper.ConvertToUserTime(entity.CreatedOnUtc, DateTimeKind.Utc).ToString("g"),
                AnsweredOnStr = entity.AnsweredOnUtc.HasValue ? _dateTimeHelper.ConvertToUserTime(entity.AnsweredOnUtc.Value, DateTimeKind.Utc).ToString("g") : string.Empty
            };

            if (entity.Product != null)
            {
                model.Product = new Nop.Web.Models.Catalog.ProductOverviewModel() { 
                    Id = entity.ProductId,
                    Name = entity.Product.Name,
                    SeName = entity.Product.GetSeName()
                };
            }

            return model;

        }

        public static List<ProductQuestionModel> ToModels(this IList<ProductQuestion> entities, IDateTimeHelper _dateTimeHelper)
        {
            var list = new List<ProductQuestionModel>();

            foreach (var entity in entities)
            {
                list.Add(entity.ToModel(_dateTimeHelper));
            }

            return list;
        }


        public static ProductQuestion ToEntity(this ProductQuestionModel model)
        {
            return new ProductQuestion() { 
                Id = model.Id,
                CustomerId = model.CustomerId,
                ProductId = model.ProductId,
                QuestionText = model.QuestionText,
                CreatedOnUtc = model.CreatedOnUtc,
                AnsweredOnUtc = model.AnsweredOnUtc
            };
        }
        #endregion
    }
}