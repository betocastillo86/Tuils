﻿using Nop.Web.Framework.Mvc;

namespace Nop.Web.Models.Catalog
{
    public partial class SearchBoxModel : BaseNopModel
    {
        public bool AutoCompleteEnabled { get; set; }
        public bool ShowProductImagesInSearchAutoComplete { get; set; }
        public int SearchTermMinimumLength { get; set; }
        public bool SearchWithSearchTerms { get; set; }

        public string CurrentSearchTerms { get; set; }
    }
}