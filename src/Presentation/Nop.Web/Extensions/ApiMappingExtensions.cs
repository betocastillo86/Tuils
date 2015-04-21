using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Infrastructure;
using Nop.Web.Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Extensions.Api
{
    public static class ApiMappingExtensions
    {
        #region ProductBaseModel
        public static Product ToEntity(this ProductBaseModel model)
        {
            var entity = new Product();
            entity.Name = model.Name;
            entity.FullDescription = model.FullDescription;
            entity.IsShipEnabled = model.IsShipEnabled;
            entity.AdditionalShippingCharge = model.AdditionalShippingCharge;
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
                model.SpecialCategories.ForEach(sc => entity.SpecialCategories.Add(sc));
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
                entity.ProductSpecificationAttributes.Add(new ProductSpecificationAttribute() { AttributeType = SpecificationAttributeType.CustomText, CustomValue = model.Kms.ToString(), SpecificationAttributeOptionId = _tuilsSettings.specificationAttributeOptionKms });

            //La placa es exclusiva de las motos
            if (!string.IsNullOrEmpty(model.CarriagePlate))
                entity.ProductSpecificationAttributes.Add(new ProductSpecificationAttribute() { AttributeType = SpecificationAttributeType.CustomText, CustomValue = model.Kms.ToString(), SpecificationAttributeOptionId = _tuilsSettings.specificationAttributeOptionCarriagePlate });

            //El año es esclusivo de las motos
            if (model.Year > 0)
                entity.Year = model.Year;

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
            
            if(model.VendorType != Core.Domain.Vendors.VendorType.User)
                attributes.Add(SystemCustomerAttributeNames.Company, model.CompanyName);

            return entity;   
        }

        #endregion


    }
}