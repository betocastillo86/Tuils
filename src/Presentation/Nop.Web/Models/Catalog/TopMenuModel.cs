﻿using System.Collections.Generic;
using Nop.Web.Framework.Mvc;

namespace Nop.Web.Models.Catalog
{
    public partial class TopMenuModel : BaseNopModel
    {
        public TopMenuModel()
        {
            Categories = new List<CategorySimpleModel>();
            Topics = new List<TopMenuTopicModel>();
        }

        public IList<CategorySimpleModel> Categories { get; set; }
        public IList<TopMenuTopicModel> Topics { get; set; }

        public IList<SpecificationAttributeOptionModel> SpecificationAttributesFilter { get; set; }

        public int SelectedSpecificationAttribute { get; set; }

        public int SelectedCategory { get; set; }

        public bool BlogEnabled { get; set; }
        public bool RecentlyAddedProductsEnabled { get; set; }
        public bool ForumEnabled { get; set; }

        #region Nested classes
        public class TopMenuTopicModel : BaseNopEntityModel
        {
            public string Name { get; set; }
            public string SeName { get; set; }
        }

        public class SpecificationAttributeOptionModel : BaseNopEntityModel
        {
            public string Name { get; set; }
            public string SeName { get; set; }
        }
        #endregion

        public bool WishlistEnabled { get; set; }

        public bool IsAuthenticated { get; set; }

        public int WishlistItems { get; set; }

        public string CustomerEmailUsername { get; set; }
    }
}