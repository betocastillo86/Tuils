using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
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
        
        public static Product ToEntity(this ProductBaseModel model)
        {
            var entity = new Product();
            entity.Name = model.Name;
            entity.FullDescription = model.FullDescription;
            entity.IsShipEnabled = model.IsShipEnabled;
            entity.AdditionalShippingCharge = model.AdditionalShippingCharge;
            entity.Price = model.Price;

            var _tuilsSettings = EngineContext.Current.Resolve<TuilsSettings>();

            if (model.ManufacturerId > 0)
                //Agrega la marca con base en ManufacturerId
                entity.ProductManufacturers.Add(new ProductManufacturer() 
                { 
                    ManufacturerId = model.ManufacturerId
                });

            //Agrega los accesorios
            if (model.Accesories != null && model.Accesories.Count > 0)
                model.Accesories
                    .ForEach(a => entity.ProductSpecificationAttributes
                                            .Add(new ProductSpecificationAttribute() {
                                                SpecificationAttributeOptionId = a,
                                                ShowOnProductPage = true
                                                }
                                            ));
            //Agrega las condiciones de negociación
            if (model.Negotiation != null && model.Negotiation.Count > 0)
                model.Negotiation
                    .ForEach(a => entity.ProductSpecificationAttributes
                                            .Add(new ProductSpecificationAttribute(){
                                                //SpecificationAttributeOptionId = _tuilsSettings.specificationAttributeNegotiation,
                                                SpecificationAttributeOptionId = a,
                                                ShowOnProductPage = true
                                                }
                                            ));
            
            //Si viene los kilometros los asocia
            if (model.Kms > 0)
                entity.ProductSpecificationAttributes.Add(new ProductSpecificationAttribute (){ AttributeType = SpecificationAttributeType.CustomText,  CustomValue = model.Kms.ToString(), SpecificationAttributeOptionId = _tuilsSettings.specificationAttributeOptionKms });
            
            if (!string.IsNullOrEmpty(model.CarriagePlate))
                entity.ProductSpecificationAttributes.Add(new ProductSpecificationAttribute() { AttributeType = SpecificationAttributeType.CustomText, CustomValue = model.Kms.ToString(), SpecificationAttributeOptionId = _tuilsSettings.specificationAttributeOptionCarriagePlate });

            //Agrega la categoria con base en CategoryId
            entity.ProductCategories.Add(new ProductCategory() 
            { 
                CategoryId = model.CategoryId
            });

            //Agrega las categorias especiales
            if(model.SpecialCategories != null)
            {
                model.SpecialCategories.ForEach(sc => entity.SpecialCategories.Add(sc));
            }

            entity.TempFiles = model.TempFiles;

            return entity;
        }
    }
}