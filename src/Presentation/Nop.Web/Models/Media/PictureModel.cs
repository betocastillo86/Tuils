using Nop.Web.Framework.Mvc;

namespace Nop.Web.Models.Media
{
    public partial class PictureModel : BaseNopEntityModel
    {

        public string ImageUrl { get; set; }

        public string FullSizeImageUrl { get; set; }

        public string Title { get; set; }

        public string AlternateText { get; set; }

        public SizePicture Size { get; set; }


        public struct SizePicture
        {
            public int Width { get; set; }

            public int Height { get; set; }
        }
    }
}