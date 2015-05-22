using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Services.Catalog;
using Nop.Services.Localization;
using Nop.Web.Framework.Mvc;
using Nop.Web.Framework.UI.Paging;

namespace Nop.Web.Models.Catalog
{
    public partial class CatalogPagingFilteringModel : BasePageableModel
    {
        #region Constructors

        public CatalogPagingFilteringModel()
        {
            this.AvailableSortOptions = new List<SelectListItem>();
            this.AvailableViewModes = new List<SelectListItem>();
            this.PageSizeOptions = new List<SelectListItem>();

            this.PriceRangeFilter = new PriceRangeFilterModel();
            this.SpecificationFilter = new SpecificationFilterModel();
            this.CategoryFilter = new CategoryFilterModel();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Price range filter model
        /// </summary>
        public PriceRangeFilterModel PriceRangeFilter { get; set; }

        /// <summary>
        /// Specification filter model
        /// </summary>
        public SpecificationFilterModel SpecificationFilter { get; set; }


        /// <summary>
        /// Category filter model
        /// </summary>
        public CategoryFilterModel CategoryFilter { get; set; }

        public bool AllowProductSorting { get; set; }
        public IList<SelectListItem> AvailableSortOptions { get; set; }

        public bool AllowProductViewModeChanging { get; set; }
        public IList<SelectListItem> AvailableViewModes { get; set; }

        public bool AllowCustomersToSelectPageSize { get; set; }
        public IList<SelectListItem> PageSizeOptions { get; set; }

        /// <summary>
        /// Order by
        /// </summary>
        public int OrderBy { get; set; }

        /// <summary>
        /// Product sorting
        /// </summary>
        public string ViewMode { get; set; }

        public string q { get; set; }
        

        #endregion

        #region Nested classes


        #region SuperClass

        public class FilterBaseModel : BaseNopModel
        {
            protected string QUERYSTRINGPARAM { get; private set; }
            
            public FilterBaseModel(string queryString)
            {
                this.QUERYSTRINGPARAM = queryString;
            }
            
            protected virtual string ExcludeQueryStringParams(string url, IWebHelper webHelper)
            {
                var excludedQueryStringParams = "pagenumber"; //remove page filtering
                if (!String.IsNullOrEmpty(excludedQueryStringParams))
                {
                    string[] excludedQueryStringParamsSplitted = excludedQueryStringParams.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string exclude in excludedQueryStringParamsSplitted)
                    {
                        url = webHelper.RemoveQueryString(url, exclude);
                    }
                }

                return url;
            }


            protected virtual string AddAsQueryStringVariable(string url, IWebHelper webHelper)
            {
                return webHelper.ModifyQueryString(url, "As=true", null);
            }

            protected virtual string GenerateFilteredQueryParam(IList<int> optionIds)
            {
                string result = "";

                if (optionIds == null || optionIds.Count == 0)
                    return result;

                for (int i = 0; i < optionIds.Count; i++)
                {
                    result += optionIds[i];
                    if (i != optionIds.Count - 1)
                        result += ",";
                }
                return result;
            }

            public virtual List<int> GetAlreadyFilteredIds(IWebHelper webHelper)
            {
                var result = new List<int>();

                var alreadyFilteredSpecsStr = webHelper.QueryString<string>(QUERYSTRINGPARAM);
                if (String.IsNullOrWhiteSpace(alreadyFilteredSpecsStr))
                    return result;

                foreach (var spec in alreadyFilteredSpecsStr.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    int specId;
                    int.TryParse(spec.Trim(), out specId);
                    if (!result.Contains(specId))
                        result.Add(specId);
                }
                return result;
            }

            
        }

        #endregion

        #region PriceRange
        public partial class PriceRangeFilterModel : FilterBaseModel
        {
            #region Const

            private const string _QUERYSTRINGPARAM = "price";

            #endregion

            #region Ctor

            public PriceRangeFilterModel()
                : base(_QUERYSTRINGPARAM)
            {
                this.Items = new List<PriceRangeFilterItem>();
            }

            #endregion

            #region Utilities

            /// <summary>
            /// Gets parsed price ranges
            /// </summary>
            protected virtual IList<PriceRange> GetPriceRangeList(string priceRangesStr)
            {
                var priceRanges = new List<PriceRange>();
                if (string.IsNullOrWhiteSpace(priceRangesStr))
                    return priceRanges;
                string[] rangeArray = priceRangesStr.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string str1 in rangeArray)
                {
                    string[] fromTo = str1.Trim().Split(new[] { '-' });

                    decimal? from = null;
                    if (!String.IsNullOrEmpty(fromTo[0]) && !String.IsNullOrEmpty(fromTo[0].Trim()))
                        from = decimal.Parse(fromTo[0].Trim(), new CultureInfo("en-US"));

                    decimal? to = null;
                    if (!String.IsNullOrEmpty(fromTo[1]) && !String.IsNullOrEmpty(fromTo[1].Trim()))
                        to = decimal.Parse(fromTo[1].Trim(), new CultureInfo("en-US"));

                    priceRanges.Add(new PriceRange { From = from, To = to });
                }
                return priceRanges;
            }



            #endregion

            #region Methods

            public virtual PriceRange GetSelectedPriceRange(IWebHelper webHelper, string priceRangesStr)
            {
                var range = webHelper.QueryString<string>(QUERYSTRINGPARAM);
                if (String.IsNullOrEmpty(range))
                    return null;
                string[] fromTo = range.Trim().Split(new[] { '-' });
                if (fromTo.Length == 2)
                {
                    decimal? from = null;
                    if (!String.IsNullOrEmpty(fromTo[0]) && !String.IsNullOrEmpty(fromTo[0].Trim()))
                        from = decimal.Parse(fromTo[0].Trim(), new CultureInfo("en-US"));
                    decimal? to = null;
                    if (!String.IsNullOrEmpty(fromTo[1]) && !String.IsNullOrEmpty(fromTo[1].Trim()))
                        to = decimal.Parse(fromTo[1].Trim(), new CultureInfo("en-US"));

                    var priceRangeList = GetPriceRangeList(priceRangesStr);
                    foreach (var pr in priceRangeList)
                    {
                        if (pr.From == from && pr.To == to)
                            return pr;
                    }
                }
                return null;
            }

            public virtual void LoadPriceRangeFilters(string priceRangeStr, IWebHelper webHelper, IPriceFormatter priceFormatter)
            {
                var priceRangeList = GetPriceRangeList(priceRangeStr);
                if (priceRangeList.Count > 0)
                {
                    this.Enabled = true;

                    var selectedPriceRange = GetSelectedPriceRange(webHelper, priceRangeStr);

                    this.Items = priceRangeList.ToList().Select(x =>
                    {
                        //from&to
                        var item = new PriceRangeFilterItem();
                        if (x.From.HasValue)
                            item.From = priceFormatter.FormatPrice(x.From.Value, true, false);
                        if (x.To.HasValue)
                            item.To = priceFormatter.FormatPrice(x.To.Value, true, false);
                        string fromQuery = string.Empty;
                        if (x.From.HasValue)
                            fromQuery = x.From.Value.ToString(new CultureInfo("en-US"));
                        string toQuery = string.Empty;
                        if (x.To.HasValue)
                            toQuery = x.To.Value.ToString(new CultureInfo("en-US"));

                        //is selected?
                        if (selectedPriceRange != null
                            && selectedPriceRange.From == x.From
                            && selectedPriceRange.To == x.To)
                            item.Selected = true;

                        //filter URL
                        string url = webHelper.ModifyQueryString(webHelper.GetThisPageUrl(true), QUERYSTRINGPARAM + "=" + fromQuery + "-" + toQuery, null);
                        url = ExcludeQueryStringParams(url, webHelper);
                        item.FilterUrl = url;


                        return item;
                    }).ToList();

                    if (selectedPriceRange != null)
                    {
                        //remove filter URL
                        string url = webHelper.RemoveQueryString(webHelper.GetThisPageUrl(true), QUERYSTRINGPARAM);
                        url = ExcludeQueryStringParams(url, webHelper);
                        this.RemoveFilterUrl = url;
                    }
                }
                else
                {
                    this.Enabled = false;
                }
            }

            #endregion

            #region Properties
            public bool Enabled { get; set; }
            public IList<PriceRangeFilterItem> Items { get; set; }
            public string RemoveFilterUrl { get; set; }

            #endregion
        }

        public partial class PriceRangeFilterItem : BaseNopModel
        {
            public string From { get; set; }
            public string To { get; set; }
            public string FilterUrl { get; set; }
            public bool Selected { get; set; }
        }

        #endregion

        #region SpecificationAttribute
        public partial class SpecificationFilterModel : FilterBaseModel
        {
            #region Const

            private const string _QUERYSTRINGPARAM = "specs";

            #endregion

            #region Ctor

            public SpecificationFilterModel()
                : base(_QUERYSTRINGPARAM)
            {
                this.AlreadyFilteredItems = new List<SpecificationFilterItem>();
                this.NotFilteredItems = new List<SpecificationFilterItem>();
            }

            #endregion

            #region Methods



            public virtual void PrepareSpecsFilters(IList<int> alreadyFilteredSpecOptionIds,
                Dictionary<int, int> filterableSpecificationAttributeOptionIds,
                ISpecificationAttributeService specificationAttributeService,
                IWebHelper webHelper,
                IWorkContext workContext)
            {
                var allFilters = new List<SpecificationAttributeOptionFilter>();
                var specificationAttributeOptions = specificationAttributeService
                    .GetSpecificationAttributeOptionsByIds(filterableSpecificationAttributeOptionIds != null ?
                    filterableSpecificationAttributeOptionIds.Keys.ToArray() : null);
                foreach (var sao in specificationAttributeOptions)
                {
                    var sa = sao.SpecificationAttribute;
                    if (sa != null)
                    {
                        allFilters.Add(new SpecificationAttributeOptionFilter
                        {
                            SpecificationAttributeId = sa.Id,
                            SpecificationAttributeName = sa.GetLocalized(x => x.Name, workContext.WorkingLanguage.Id),
                            SpecificationAttributeDisplayOrder = sa.DisplayOrder,
                            SpecificationAttributeOptionId = sao.Id,
                            SpecificationAttributeOptionName = sao.GetLocalized(x => x.Name, workContext.WorkingLanguage.Id),
                            SpecificationAttributeOptionDisplayOrder = sao.DisplayOrder
                        });
                    }
                }

                //sort loaded options
                allFilters = allFilters.OrderBy(saof => saof.SpecificationAttributeDisplayOrder)
                    .ThenBy(saof => saof.SpecificationAttributeName)
                    .ThenBy(saof => saof.SpecificationAttributeOptionDisplayOrder)
                    .ThenBy(saof => saof.SpecificationAttributeOptionName).ToList();

                //get already filtered specification options
                var alreadyFilteredOptions = allFilters
                    .Where(x => alreadyFilteredSpecOptionIds.Contains(x.SpecificationAttributeOptionId))
                    .Select(x => x)
                    .ToList();

                //get not filtered specification options
                var notFilteredOptions = new List<SpecificationAttributeOptionFilter>();
                foreach (var saof in allFilters)
                {
                    //do not add already filtered specification options
                    if (alreadyFilteredOptions.FirstOrDefault(x => x.SpecificationAttributeId == saof.SpecificationAttributeId) != null)
                        continue;

                    //else add it
                    notFilteredOptions.Add(saof);
                }

                //prepare the model properties
                if (alreadyFilteredOptions.Count > 0 || notFilteredOptions.Count > 0)
                {
                    this.Enabled = true;

                    this.AlreadyFilteredItems = alreadyFilteredOptions.ToList().Select(x =>
                    {
                        var item = new SpecificationFilterItem();
                        item.SpecificationAttributeName = x.SpecificationAttributeName;
                        item.SpecificationAttributeOptionName = x.SpecificationAttributeOptionName;

                        return item;
                    }).ToList();

                    this.NotFilteredItems = notFilteredOptions.ToList().Select(x =>
                    {
                        var item = new SpecificationFilterItem();
                        item.SpecificationAttributeName = x.SpecificationAttributeName;
                        item.SpecificationAttributeOptionName = x.SpecificationAttributeOptionName;
                        item.NumOfProducts = filterableSpecificationAttributeOptionIds.FirstOrDefault(s => s.Key == x.SpecificationAttributeOptionId).Value;

                        //filter URL
                        var alreadyFilteredOptionIds = GetAlreadyFilteredIds(webHelper);
                        if (!alreadyFilteredOptionIds.Contains(x.SpecificationAttributeOptionId))
                            alreadyFilteredOptionIds.Add(x.SpecificationAttributeOptionId);
                        string newQueryParam = GenerateFilteredQueryParam(alreadyFilteredOptionIds);
                        string filterUrl = webHelper.ModifyQueryString(webHelper.GetThisPageUrl(true), QUERYSTRINGPARAM + "=" + newQueryParam, null);
                        filterUrl = ExcludeQueryStringParams(filterUrl, webHelper);
                        filterUrl = AddAsQueryStringVariable(filterUrl, webHelper);
                        item.FilterUrl = filterUrl;

                        return item;
                    }).ToList();


                    //remove filter URL
                    string removeFilterUrl = webHelper.RemoveQueryString(webHelper.GetThisPageUrl(true), QUERYSTRINGPARAM);
                    removeFilterUrl = ExcludeQueryStringParams(removeFilterUrl, webHelper);
                    removeFilterUrl = AddAsQueryStringVariable(removeFilterUrl, webHelper);
                    this.RemoveFilterUrl = removeFilterUrl;
                }
                else
                {
                    this.Enabled = false;
                }
            }

            #endregion

            #region Properties
            public bool Enabled { get; set; }
            public IList<SpecificationFilterItem> AlreadyFilteredItems { get; set; }
            public IList<SpecificationFilterItem> NotFilteredItems { get; set; }
            public string RemoveFilterUrl { get; set; }

            #endregion
        }

        public partial class SpecificationFilterItem : BaseNopModel
        {
            public string SpecificationAttributeName { get; set; }
            public string SpecificationAttributeOptionName { get; set; }

            public int NumOfProducts { get; set; }

            public string FilterUrl { get; set; }
        }

        #endregion

        
        public partial class CategoryFilterModel : FilterBaseModel
        { 
            #region Const

            private const string _QUERYSTRINGPARAM = "Cid";

            #endregion

            #region Ctor

            public CategoryFilterModel()
                : base(_QUERYSTRINGPARAM)
            {
                this.AlreadyFilteredItems = new List<CategoryFilterItem>();
                this.NotFilteredItems = new List<CategoryFilterItem>();
            }

            #endregion

            #region Methods

           

            public virtual void PrepareCategoriesFilters(IList<int> alreadyCategoryIds,
                Dictionary<int, int> filterableCategoryIds,
                ICategoryService categoryService, 
                IWebHelper webHelper,
                IWorkContext workContext)
            {
                //var allFilters = new List<CategoryModel>();
                var categoryOptions = categoryService
                    .GetCategoriesByIds(filterableCategoryIds != null ?
                    filterableCategoryIds.Keys.ToArray() : new int[]{});
                var allFilters = categoryOptions;
                
                //foreach (var cao in categoryOptions)
                //{
                //    allFilters.Add(new CategoryModel() { 
                //        Id = cao.Id,
                //        Name = cao.Name,
                //        order
                //    });
                //}

                //sort loaded options
                allFilters = allFilters.OrderBy(c => c.DisplayOrder)
                    .ThenBy(saof => saof.Name).ToList();
                
                //get already filtered categpries
                var alreadyFilteredOptions = allFilters
                    .Where(x => alreadyCategoryIds.Contains(x.Id))
                    .Select(x => x)
                    .ToList();

                //get not filtered specification options
                var notFilteredOptions = new List<Category>();
                foreach (var cat in allFilters)
                {
                    //do not add already filtered specification options
                    if (alreadyFilteredOptions.FirstOrDefault(x => x.Id == cat.Id) != null)
                        continue;

                    //else add it
                    notFilteredOptions.Add(cat);
                }

                //prepare the model properties
                if (alreadyFilteredOptions.Count > 0 || notFilteredOptions.Count > 0)
                {
                    this.Enabled = true;
                    
                    this.AlreadyFilteredItems = alreadyFilteredOptions.ToList().Select(x =>
                    {
                        var item = new CategoryFilterItem();
                        item.CategoryName = x.Name;
                        return item;
                    }).ToList();

                    this.NotFilteredItems = notFilteredOptions.ToList().Select(x =>
                    {
                        var item = new CategoryFilterItem();
                        item.CategoryName = x.Name;
                        item.NumOfProducts = filterableCategoryIds.FirstOrDefault(s => s.Key == x.Id).Value; 

                        //filter URL
                        var alreadyFilteredCategoryIds = GetAlreadyFilteredIds(webHelper);
                        if (!alreadyFilteredCategoryIds.Contains(x.Id))
                            alreadyFilteredCategoryIds.Add(x.Id);
                        string newQueryParam = GenerateFilteredQueryParam(alreadyFilteredCategoryIds);
                        string filterUrl = webHelper.ModifyQueryString(webHelper.GetThisPageUrl(true), QUERYSTRINGPARAM + "=" + newQueryParam, null);
                        filterUrl = ExcludeQueryStringParams(filterUrl, webHelper);
                        filterUrl = AddAsQueryStringVariable(filterUrl, webHelper);
                        item.FilterUrl = filterUrl;
                        
                        return item;
                    }).ToList();


                    //remove filter URL
                    string removeFilterUrl = webHelper.RemoveQueryString(webHelper.GetThisPageUrl(true), QUERYSTRINGPARAM);
                    removeFilterUrl = ExcludeQueryStringParams(removeFilterUrl, webHelper);
                    removeFilterUrl = AddAsQueryStringVariable(removeFilterUrl, webHelper);
                    this.RemoveFilterUrl = removeFilterUrl;

                }
                else
                {
                    this.Enabled = false;
                }
            }

            #endregion

            #region Properties
            public bool Enabled { get; set; }
            public IList<CategoryFilterItem> AlreadyFilteredItems { get; set; }
            public IList<CategoryFilterItem> NotFilteredItems { get; set; }
            public string RemoveFilterUrl { get; set; }

            #endregion
        }

        public partial class CategoryFilterItem : BaseNopModel
        {
            public string CategoryName { get; set; }
            public int NumOfProducts { get; set; }
            public string FilterUrl { get; set; }
        }

        #endregion
    }
}