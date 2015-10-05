using Nop.Web.Framework.Mvc;
using Nop.Web.Models.Catalog;
using System.Collections.Generic;

namespace Nop.Web.Models.Common
{
    public partial class HeaderLinksModel : BaseNopModel
    {
        public bool IsAuthenticated { get; set; }
        public string CustomerEmailUsername { get; set; }
        
        public bool ShoppingCartEnabled { get; set; }
        public int ShoppingCartItems { get; set; }
        
        public bool WishlistEnabled { get; set; }
        public int WishlistItems { get; set; }

        public bool AllowPrivateMessages { get; set; }
        public string UnreadPrivateMessages { get; set; }
        public string AlertMessage { get; set; }

        public int UnansweredQuestions { get; set; }

        public List<CategorySimpleModel> Categories { get; set; }

        public string YoutubeLink { get; set; }

        public string FacebookLink { get; set; }

    }
}