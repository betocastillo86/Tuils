using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Media;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Web.Models.Catalog;
using Nop.Web.Models.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nop.Services.Seo;

namespace Nop.Web.Extensions
{
    //Extensiones que permiten traer datos de las imagenes de los diferentes elementos
    public static class PictureModelExtensions
    {
        /// <summary>
        /// Retorna la imagen por defecto de una categoria
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="localizationService"></param>
        /// <param name="mediaSettings"></param>
        /// <param name="pictureService"></param>
        /// <returns></returns>
        public static PictureModel GetPicture(this Category entity, 
            ILocalizationService localizationService, 
            MediaSettings mediaSettings,
            IPictureService pictureService)
        {
            
            int pictureSize = mediaSettings.CategoryThumbPictureSize;
            var picture = pictureService.GetPictureById(entity.PictureId);

            var subCatModel = new CategoryModel.SubCategoryModel
            {
                Id = entity.Id,
                Name = entity.GetLocalized(y => y.Name),
                SeName = entity.GetSeName()
            };

            return  new PictureModel
            {
                FullSizeImageUrl = pictureService.GetPictureUrl(picture),
                ImageUrl = pictureService.GetPictureUrl(picture, pictureSize),
                Title = string.Format(localizationService.GetResource("Media.Category.ImageLinkTitleFormat"), subCatModel.Name),
                AlternateText = string.Format(localizationService.GetResource("Media.Category.ImageAlternateTextFormat"), subCatModel.Name)
            };
        }


        /// <summary>
        /// Carga la imagen de un producto
        /// </summary>
        /// <param name="product"></param>
        /// <param name="localizationService"></param>
        /// <param name="mediaSettings"></param>
        /// <param name="pictureService"></param>
        /// <param name="pictureSize"></param>
        /// <returns></returns>
        public static PictureModel GetPicture(this Product product,
            ILocalizationService localizationService,
            MediaSettings mediaSettings,
            IPictureService pictureService,
            int? pictureSize = null)
        {
            if (!pictureSize.HasValue)
                pictureSize = mediaSettings.ProductThumbPictureSize;

            var picture = pictureService.GetPicturesByProductId(product.Id, 1).FirstOrDefault();
            var pictureModel = new PictureModel
            {
                ImageUrl = pictureService.GetPictureUrl(picture, pictureSize.Value),
                FullSizeImageUrl = pictureService.GetPictureUrl(picture),
                Title = string.Format(localizationService.GetResource("Media.Product.ImageLinkTitleFormat"), product.Name),
                AlternateText = string.Format(localizationService.GetResource("Media.Product.ImageAlternateTextFormat"), product.Name)
            };
            return pictureModel;
        }

        /// <summary>
        /// Carga la imagen de una marca
        /// </summary>
        /// <param name="manufacturer"></param>
        /// <param name="localizationService"></param>
        /// <param name="mediaSettings"></param>
        /// <param name="pictureService"></param>
        /// <param name="pictureSize"></param>
        /// <returns></returns>
        public static PictureModel GetPicture(this Manufacturer manufacturer,
            ILocalizationService localizationService,
            MediaSettings mediaSettings,
            IPictureService pictureService,
            int? pictureSize = null)
        {
            if (!pictureSize.HasValue)
                pictureSize = mediaSettings.ManufacturerThumbPictureSize;

            var picture = pictureService.GetPictureById(manufacturer.PictureId);
            var pictureModel = new PictureModel
            {
                FullSizeImageUrl = pictureService.GetPictureUrl(picture),
                ImageUrl = pictureService.GetPictureUrl(picture, pictureSize.Value),
                Title = string.Format(localizationService.GetResource("Media.Manufacturer.ImageLinkTitleFormat"), manufacturer.Name),
                AlternateText = string.Format(localizationService.GetResource("Media.Manufacturer.ImageAlternateTextFormat"), manufacturer.Name)
            };
            return pictureModel;
        }
    }
}