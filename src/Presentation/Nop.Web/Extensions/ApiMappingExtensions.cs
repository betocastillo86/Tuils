using Nop.Core.Domain.Catalog;
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


            //Agrega la marca con base en ManufacturerId
            entity.ProductManufacturers.Add(new ProductManufacturer() 
            { 
                ManufacturerId = model.ManufacturerId
            });

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