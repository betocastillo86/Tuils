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
using Nop.Services.Directory;
using Nop.Core.Domain.Directory;
using System.Text;
using Nop.Services.Seo;

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
            this.StateProvinceFilter = new StateProvinceFilterModel();
            this.ManufacturerFilter = new ManufacturerFilterModel();
            this.BikeReferenceFilter = new BikeReferenceFilterModel();
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
        /// state province filter
        /// </summary>
        public StateProvinceFilterModel StateProvinceFilter { get; set; }

        /// <summary>
        /// filtro por marca
        /// </summary>
        public ManufacturerFilterModel ManufacturerFilter { get; set; }


        public BikeReferenceFilterModel BikeReferenceFilter { get; set; }


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

            public bool ShowFilterNameInUrl { get; set; }


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

            /// <summary>
            /// Crea la Url para los filtros
            /// </summary>
            /// <param name="webHelper">Url que se está modificando</param>
            /// <param name="currentId">Id que se está procesando en el momento</param>
            /// <param name="currentName">Nombre que se está procesando en el momento</param>
            /// <returns></returns>
            protected string CreateFilterUrl(IWebHelper webHelper, int currentId, string currentName)
            {
                var alreadyFilteredOptionIds = GetAlreadyFilteredIds(webHelper);
                if (!alreadyFilteredOptionIds.Contains(currentId))
                    alreadyFilteredOptionIds.Add(currentId);

                string newQueryParam = GenerateFilteredQueryParam(alreadyFilteredOptionIds);

                string filterUrl = webHelper.ModifyQueryString(webHelper.GetThisPageUrl(true), string.Format("{0}={1}", QUERYSTRINGPARAM, newQueryParam), null);

                //Si el nombre viene lo agrega al queryString
                if(ShowFilterNameInUrl && currentName != null)
                    filterUrl = webHelper.AddToRouteValues(filterUrl, SeoExtensions.GetSeName(currentName));

                //Elimina los parametros sobrantes
                filterUrl = ExcludeQueryStringParams(filterUrl, webHelper);

                return filterUrl;
            }
            
            /// <summary>
            /// Elimina un filtro del listado. Tanto de querystring como de parte de la Url
            /// </summary>
            /// <param name="webHelper"></param>
            /// <param name="currentName">parametros de la Url que debe remover</param>
            /// <returns></returns>
            protected string RemoveFilterFromUrl(IWebHelper webHelper, params string[] namesToRemove)
            {
                //Elimina del query string la varibale
                string removeFilterUrl = webHelper.RemoveQueryString(webHelper.GetThisPageUrl(true), QUERYSTRINGPARAM);
                removeFilterUrl = ExcludeQueryStringParams(removeFilterUrl, webHelper);
                //Si debe eliminar nombres de la raiz de la URL los saca tambien
                if (ShowFilterNameInUrl && namesToRemove != null && namesToRemove.Length > 0)
                    removeFilterUrl = webHelper.RemoveRouteValues(removeFilterUrl, namesToRemove);

                return removeFilterUrl;
            }



            protected virtual string GenerateFilteredQueryParam<T>(IList<T> optionIds)
            {
                return string.Join(",", optionIds);
                
                //string result = "";

                //if (optionIds == null || optionIds.Count == 0)
                //    return result;

                //for (int i = 0; i < optionIds.Count; i++)
                //{
                //    result += optionIds[i];
                //    if (i != optionIds.Count - 1)
                //        result += ",";
                //}
                //return result;
            }

            /// <summary>
            /// Retorna los ids que ya han sido filtradps
            /// </summary>
            /// <param name="webHelper"></param>
            /// <returns></returns>
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

            public virtual int? GetAlreadyFilteredId(IWebHelper webHelper)
            {
                var alreadyFilteredStr = webHelper.QueryString<string>(QUERYSTRINGPARAM);
                if (String.IsNullOrWhiteSpace(alreadyFilteredStr))
                    return null;

                int id = 0;

                if (int.TryParse(alreadyFilteredStr, out id))
                    return id;
                else
                    return null;
            }


        }

        public class FilterBaseItem : BaseNopModel
        {
            public string Name { get; set; }
            public int NumOfProducts { get; set; }
            public string FilterUrl { get; set; }
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

                    if (string.IsNullOrEmpty(priceRangesStr))
                    {
                        return new PriceRange() { From = from, To = to };
                    }
                    else
                    {
                        var priceRangeList = GetPriceRangeList(priceRangesStr);
                        foreach (var pr in priceRangeList)
                        {
                            if (pr.From == from && pr.To == to)
                                return pr;
                        }
                    }
                }
                return null;
            }

            /// <summary>
            /// Calcula los rangos de precio dependiendo del más barato y el más caro
            /// </summary>
            /// <param name="minPrice"></param>
            /// <param name="maxPrice"></param>
            /// <param name="webHelper"></param>
            /// <param name="priceFormatter"></param>
            public virtual void LoadPriceRangeFilters(int minPrice, int maxPrice, IWebHelper webHelper, IPriceFormatter priceFormatter)
            {

                var diferencePrices = (double)(maxPrice - minPrice);
                //Si la diferencia de precios es menor a 50000 no debe mostrar el filtro
                if (diferencePrices < 50000)
                {
                    //Valida si se ha seleccionado un precio anteriormente
                    var selectedPrice = this.GetSelectedPriceRange(webHelper, null);
                    this.Enabled = selectedPrice != null;

                    //Si hay precio seleccionado carga los valores
                    if (selectedPrice != null)
                    {
                        var priceItem = new PriceRangeFilterItem() { Selected = true };
                        if (selectedPrice.From.HasValue)
                            priceItem.From = priceFormatter.FormatPrice(selectedPrice.From.Value, true, false);

                        if (selectedPrice.To.HasValue)
                            priceItem.To = priceFormatter.FormatPrice(selectedPrice.To.Value, true, false);

                        this.Items.Add(priceItem);

                        //string url = webHelper.RemoveQueryString(webHelper.GetThisPageUrl(true), QUERYSTRINGPARAM);
                        //url = ExcludeQueryStringParams(url, webHelper);
                        //this.RemoveFilterUrl = url;

                        this.RemoveFilterUrl = this.RemoveFilterFromUrl(webHelper, null);
                    }
                }
                else
                {
                    var priceRangeStr = new StringBuilder();

                    //Muestra 4 rangos de precios
                    double prevMin = Math.Round(minPrice / 1000d, 0) * 1000d;
                    double prevMax = minPrice;
                    for (int i = 4; i > 0; i--)
                    {
                        //Toma el primer rango y lo suma a la lista aproximandolo a miles de pesos
                        double rangePriceDiference = Math.Ceiling(diferencePrices / i);
                        //Toma el menor precio y le suma el total del rango
                        prevMax = Math.Round((minPrice + rangePriceDiference) / 1000d, 0) * 1000d;
                        //Carga el menor precio menos 1 y el mayor precio del rango
                        priceRangeStr.AppendFormat("{0}-{1};", prevMin, i > 1 ? (prevMax).ToString() : string.Empty);
                        prevMin = prevMax;
                    }

                    LoadPriceRangeFilters(priceRangeStr.ToString(), webHelper, priceFormatter);
                }
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

                    //Filtro que se debe ejecutar cuando un usuario pone el rango de tiempo
                    string customUrl = webHelper.ModifyQueryString(webHelper.GetThisPageUrl(true), QUERYSTRINGPARAM + "={from}-{to}", null);
                    customUrl = ExcludeQueryStringParams(customUrl, webHelper);
                    this.CustomFilterUrl = customUrl;

                    if (selectedPriceRange != null)
                    {
                        //remove filter URL
                        //string url = webHelper.RemoveQueryString(webHelper.GetThisPageUrl(true), QUERYSTRINGPARAM);
                        //url = ExcludeQueryStringParams(url, webHelper);
                        //this.RemoveFilterUrl = url;
                        this.RemoveFilterUrl = this.RemoveFilterFromUrl(webHelper, null);
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

            public string CustomFilterUrl { get; set; }

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

            public virtual void Add(int specId, ISpecificationAttributeService specificationAttributeService, IWorkContext workContext)
            {
                var sao = specificationAttributeService.GetSpecificationAttributeOptionById(specId);
                this.AlreadyFilteredItems.Add(
                    new SpecificationFilterItem()
                    {
                        Name = sao.SpecificationAttribute.Name,
                        SpecificationAttributeOptionName = sao.Name
                    }
                );
            }

            public virtual void PrepareSpecsFilters(IList<int> alreadyFilteredSpecOptionIds,
                Dictionary<int, int> filterableSpecificationAttributeOptionIds,
                ISpecificationAttributeService specificationAttributeService,
                IWebHelper webHelper,
                IWorkContext workContext,
                //Valida si debe agregar el filtro del nombre a las URL
                bool addFilterNameToUrl = true)
            {
                this.ShowFilterNameInUrl = addFilterNameToUrl;
                
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
                        item.Name = x.SpecificationAttributeName;
                        item.SpecificationAttributeOptionName = x.SpecificationAttributeOptionName;

                        return item;
                    }).ToList();

                    this.NotFilteredItems = notFilteredOptions.ToList().Select(x =>
                    {
                        var item = new SpecificationFilterItem();
                        item.Name = x.SpecificationAttributeName;
                        item.SpecificationAttributeOptionName = x.SpecificationAttributeOptionName;
                        item.NumOfProducts = filterableSpecificationAttributeOptionIds.FirstOrDefault(s => s.Key == x.SpecificationAttributeOptionId).Value;

                        ////filter URL
                        //var alreadyFilteredOptionIds = GetAlreadyFilteredIds(webHelper);
                        //var alreadyFilteredOptionNames = GetAlreadyFilteredNames(webHelper);
                        //if (!alreadyFilteredOptionIds.Contains(x.SpecificationAttributeOptionId))
                        //{
                        //    alreadyFilteredOptionIds.Add(x.SpecificationAttributeOptionId);
                        //    alreadyFilteredOptionNames.Add(x.SpecificationAttributeOptionName);
                        //}

                        //string newQueryParam = GenerateFilteredQueryParam(alreadyFilteredOptionIds);
                        //string newQueryParamName = GenerateFilteredQueryParam(alreadyFilteredOptionNames);

                        //string filterUrl = webHelper.ModifyQueryString(webHelper.GetThisPageUrl(true), string.Format("{0}={1}&{2}={3}", QUERYSTRINGPARAM, newQueryParam, QUERYSTRINGPARAM_TEXT, newQueryParamName), null);
                        
                        //filterUrl = ExcludeQueryStringParams(filterUrl, webHelper);
                        //item.FilterUrl = filterUrl;
                        item.FilterUrl = this.CreateFilterUrl(webHelper, x.SpecificationAttributeOptionId, null);

                        return item;
                    }).ToList();

                    this.RemoveFilterUrl = this.RemoveFilterFromUrl(webHelper, alreadyFilteredOptions.Select(f => SeoExtensions.GetSeName(f.SpecificationAttributeOptionName)).ToArray());
                    //remove filter URL
                    //string removeFilterUrl = webHelper.RemoveQueryString(webHelper.GetThisPageUrl(true), QUERYSTRINGPARAM, QUERYSTRINGPARAM_TEXT);
                    //removeFilterUrl = ExcludeQueryStringParams(removeFilterUrl, webHelper);
                    //this.RemoveFilterUrl = removeFilterUrl;
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

        public partial class SpecificationFilterItem : FilterBaseItem
        {
            public string SpecificationAttributeOptionName { get; set; }
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
                this.AlreadyFilteredItems = new List<FilterBaseItem>();
                this.NotFilteredItems = new List<FilterBaseItem>();
            }

            #endregion

            #region Methods



            public virtual void PrepareCategoriesFilters(IList<int> alreadyCategoryIds,
                Dictionary<int, int> filterableCategoryIds,
                ICategoryService categoryService,
                IWebHelper webHelper,
                IWorkContext workContext,
                //Valida si debe agregar el filtro del nombre a las URL
                bool addFilterNameToUrl = true)
            {
                this.Enabled = true;
                this.ShowFilterNameInUrl = addFilterNameToUrl;

                if (alreadyCategoryIds.Count > 0)
                {
                    var selectedCategory = categoryService.GetCategoryById(alreadyCategoryIds.FirstOrDefault());
                    this.AlreadyFilteredItems.Add(new FilterBaseItem()
                    {
                        Name = selectedCategory != null ? selectedCategory.Name : string.Empty
                    });

                    //remove filter URL
                    //string removeFilterUrl = webHelper.RemoveQueryString(webHelper.GetThisPageUrl(true), QUERYSTRINGPARAM, QUERYSTRINGPARAM_TEXT);
                    //removeFilterUrl = ExcludeQueryStringParams(removeFilterUrl, webHelper);
                    //this.RemoveFilterUrl = removeFilterUrl;

                    this.RemoveFilterUrl = this.RemoveFilterFromUrl(webHelper, this.AlreadyFilteredItems.Select(f => SeoExtensions.GetSeName(f.Name)).ToArray());
                }
                else if (filterableCategoryIds.Count > 1)
                {
                    //Carga las categorias
                    var options = categoryService
                        .GetCategoriesByIds(filterableCategoryIds != null ?
                        filterableCategoryIds.Keys.ToArray() : new int[] { });

                    this.NotFilteredItems = options.ToList().Select(x =>
                       {
                           var item = new FilterBaseItem();
                           item.Name = x.Name;
                           item.NumOfProducts = filterableCategoryIds.FirstOrDefault(s => s.Key == x.Id).Value;

                           //filter URL
                           //var alreadyFilteredCategoryIds = GetAlreadyFilteredIds(webHelper);
                           //var alreadyFilteredCategoryNames = GetAlreadyFilteredNames(webHelper);
                           //if (!alreadyFilteredCategoryIds.Contains(x.Id))
                           //{
                           //    alreadyFilteredCategoryIds.Add(x.Id);
                           //    alreadyFilteredCategoryNames.Add(x.Name);
                           //}
                               
                           //string newQueryParam = GenerateFilteredQueryParam(alreadyFilteredCategoryIds);
                           //string newQueryParamName = GenerateFilteredQueryParam(alreadyFilteredCategoryNames);
                           //string filterUrl = webHelper.ModifyQueryString(webHelper.GetThisPageUrl(true), string.Format("{0}={1}&{2}={3}", QUERYSTRINGPARAM, newQueryParam, QUERYSTRINGPARAM_TEXT, newQueryParamName), null);
                           //filterUrl = ExcludeQueryStringParams(filterUrl, webHelper);
                           //item.FilterUrl = filterUrl;
                           item.FilterUrl = this.CreateFilterUrl(webHelper, x.Id, x.Name);

                           return item;
                       }).ToList();

                }
                else
                {
                    this.Enabled = false;
                }

            }

            #endregion

            #region Properties
            public bool Enabled { get; set; }
            public IList<FilterBaseItem> AlreadyFilteredItems { get; set; }
            public IList<FilterBaseItem> NotFilteredItems { get; set; }
            public string RemoveFilterUrl { get; set; }

            #endregion
        }


        #endregion
        #region StateProvince

        public partial class StateProvinceFilterModel : FilterBaseModel
        {
            #region Const

            private const string _QUERYSTRINGPARAM = "sp";

            #endregion

            #region Ctor

            public StateProvinceFilterModel()
                : base(_QUERYSTRINGPARAM)
            {
            }

            #endregion

            #region Methods



            public virtual void PrepareStateProvinceFilters(int? selectedProvinceId,
                Dictionary<int, int> filterableStateProvinceIds,
                IStateProvinceService stateProvinceService,
                IWebHelper webHelper,
                IWorkContext workContext,
                //Valida si debe agregar el filtro del nombre a las URL
                bool addFilterNameToUrl = true)
            {

                this.ShowFilterNameInUrl = addFilterNameToUrl;

                List<StateProvince> statesOptions = null;

                if (!selectedProvinceId.HasValue)
                {
                    statesOptions = stateProvinceService
                     .GetStatesProvincesByIds(filterableStateProvinceIds != null ?
                     filterableStateProvinceIds.Keys.ToArray() : new int[] { })
                     .OrderBy(c => c.DisplayOrder)
                     .ThenBy(saof => saof.Name)
                     .ToList();
                }


                //prepare the model properties
                if (selectedProvinceId.HasValue || statesOptions.Count > 1)
                {
                    this.Enabled = true;


                    if (selectedProvinceId.HasValue)
                    {
                        this.FilteredItem = new FilterBaseItem() { Name = stateProvinceService.GetStateProvinceById(selectedProvinceId.Value).Name };
                        this.RemoveFilterUrl = this.RemoveFilterFromUrl(webHelper, SeoExtensions.GetSeName(FilteredItem.Name));
                    }
                    else
                    {
                        this.NotFilteredItems = statesOptions.Select(x =>
                        {
                            var item = new FilterBaseItem();
                            item.Name = x.Name;
                            item.NumOfProducts = filterableStateProvinceIds.FirstOrDefault(s => s.Key == x.Id).Value;

                            //filter URL
                            //var alreadyFilteredCategoryIds = GetAlreadyFilteredIds(webHelper);
                            //if (!alreadyFilteredCategoryIds.Contains(x.Id))
                            //    alreadyFilteredCategoryIds.Add(x.Id);
                            //string newQueryParam = GenerateFilteredQueryParam(alreadyFilteredCategoryIds);
                            //string filterUrl = webHelper.ModifyQueryString(webHelper.GetThisPageUrl(true), QUERYSTRINGPARAM + "=" + newQueryParam, null);
                            //filterUrl = ExcludeQueryStringParams(filterUrl, webHelper);
                            //item.FilterUrl = filterUrl;

                            item.FilterUrl = this.CreateFilterUrl(webHelper, x.Id, x.Name);

                            return item;
                        }).ToList();
                    }

                    //remove filter URL
                    ////string removeFilterUrl = webHelper.RemoveQueryString(webHelper.GetThisPageUrl(true), QUERYSTRINGPARAM);
                    ////removeFilterUrl = ExcludeQueryStringParams(removeFilterUrl, webHelper);
                    ////this.RemoveFilterUrl = removeFilterUrl;

                    

                }
                else
                {
                    this.Enabled = false;
                }
            }

            #endregion

            #region Properties
            public bool Enabled { get; set; }

            public IList<FilterBaseItem> NotFilteredItems { get; set; }

            public FilterBaseItem FilteredItem { get; set; }

            public string RemoveFilterUrl { get; set; }

            #endregion
        }

        #endregion

        #region Manufacturer

        public partial class ManufacturerFilterModel : FilterBaseModel
        {
            #region Const

            private const string _QUERYSTRINGPARAM = "mid";

            #endregion

            #region Ctor

            public ManufacturerFilterModel()
                : base(_QUERYSTRINGPARAM)
            {
            }

            #endregion

            #region Methods



            public virtual void PrepareFilters(int? manufactutrerId,
                Dictionary<int, int> filterableManufacturerIds,
                IManufacturerService manufacturerService,
                IWebHelper webHelper,
                IWorkContext workContext,
                //Valida si debe agregar el filtro del nombre a las URL
                bool addFilterNameToUrl = true)
            {
                this.ShowFilterNameInUrl = addFilterNameToUrl;

                List<Manufacturer> options = null;

                if (!manufactutrerId.HasValue)
                {
                    options = manufacturerService
                     .GetManufacturersByIds(filterableManufacturerIds != null ?
                     filterableManufacturerIds.Keys.ToArray() : new int[] { })
                     .OrderBy(c => c.DisplayOrder)
                     .ThenBy(saof => saof.Name)
                     .ToList();
                }


                //prepare the model properties
                if (manufactutrerId.HasValue || options.Count > 1)
                {
                    this.Enabled = true;

                    if (manufactutrerId.HasValue)
                    {
                        this.FilteredItem = new FilterBaseItem() { Name = manufacturerService.GetManufacturerById(manufactutrerId.Value).Name };
                        this.RemoveFilterUrl = this.RemoveFilterFromUrl(webHelper, SeoExtensions.GetSeName(FilteredItem.Name));
                    }
                    else
                    {
                        this.NotFilteredItems = options.Select(x =>
                        {
                            var item = new FilterBaseItem();
                            item.Name = x.Name;
                            item.NumOfProducts = filterableManufacturerIds.FirstOrDefault(s => s.Key == x.Id).Value;

                            //filter URL
                            //var alreadyFilteredCategoryIds = GetAlreadyFilteredIds(webHelper);
                            //if (!alreadyFilteredCategoryIds.Contains(x.Id))
                            //    alreadyFilteredCategoryIds.Add(x.Id);
                            //string newQueryParam = GenerateFilteredQueryParam(alreadyFilteredCategoryIds);
                            //string filterUrl = webHelper.ModifyQueryString(webHelper.GetThisPageUrl(true), QUERYSTRINGPARAM + "=" + newQueryParam, null);
                            //filterUrl = ExcludeQueryStringParams(filterUrl, webHelper);
                            //item.FilterUrl = filterUrl;
                            item.FilterUrl = this.CreateFilterUrl(webHelper, x.Id, x.Name);

                            return item;
                        }).ToList();
                    }




                    //remove filter URL
                    //string removeFilterUrl = webHelper.RemoveQueryString(webHelper.GetThisPageUrl(true), QUERYSTRINGPARAM);
                    //removeFilterUrl = ExcludeQueryStringParams(removeFilterUrl, webHelper);
                    //this.RemoveFilterUrl = removeFilterUrl;

                }
                else
                {
                    this.Enabled = false;
                }
            }

            #endregion

            #region Properties
            public bool Enabled { get; set; }

            public IList<FilterBaseItem> NotFilteredItems { get; set; }

            public FilterBaseItem FilteredItem { get; set; }

            public string RemoveFilterUrl { get; set; }

            #endregion
        }

        #endregion

        #region BikeReference

        public partial class BikeReferenceFilterModel : FilterBaseModel
        {
            #region Const

            private const string _QUERYSTRINGPARAM = "br";

            #endregion

            #region Ctor

            public BikeReferenceFilterModel()
                : base(_QUERYSTRINGPARAM)
            {
            }

            #endregion

            #region Methods



            public virtual void PrepareFilters(int? specialCategoryId,
                Dictionary<int, int> filterableSpecialCategoryIds,
                ICategoryService categoryService,
                IWebHelper webHelper,
                IWorkContext workContext,
                //Valida si debe agregar el filtro del nombre a las URL
                bool addFilterNameToUrl = false)
            {
                this.ShowFilterNameInUrl = addFilterNameToUrl;
                
                List<Category> options = null;

                if (!specialCategoryId.HasValue)
                {
                    options = categoryService
                     .GetCategoriesByIds(filterableSpecialCategoryIds != null ?
                     filterableSpecialCategoryIds.Keys.ToArray() : new int[] { })
                     .OrderBy(c => c.DisplayOrder)
                     .ThenBy(saof => saof.Name)
                     .ToList();
                }


                //prepare the model properties
                if (specialCategoryId.HasValue || options.Count > 1)
                {
                    this.Enabled = true;

                    if (specialCategoryId.HasValue)
                    {
                        this.FilteredItem = new FilterBaseItem() { Name = categoryService.GetCategoryById(specialCategoryId.Value).Name };
                        this.RemoveFilterUrl = this.RemoveFilterFromUrl(webHelper, SeoExtensions.GetSeName(FilteredItem.Name));
                    }
                    else
                    {
                        this.NotFilteredItems = options.Select(x =>
                        {
                            var item = new FilterBaseItem();
                            item.Name = x.Name;
                            item.NumOfProducts = filterableSpecialCategoryIds.FirstOrDefault(s => s.Key == x.Id).Value;

                            //filter URL
                            //var alreadyFilteredCategoryIds = GetAlreadyFilteredIds(webHelper);
                            //if (!alreadyFilteredCategoryIds.Contains(x.Id))
                            //    alreadyFilteredCategoryIds.Add(x.Id);
                            //string newQueryParam = GenerateFilteredQueryParam(alreadyFilteredCategoryIds);
                            //string filterUrl = webHelper.ModifyQueryString(webHelper.GetThisPageUrl(true), QUERYSTRINGPARAM + "=" + newQueryParam, null);
                            //filterUrl = ExcludeQueryStringParams(filterUrl, webHelper);
                            //item.FilterUrl = filterUrl;
                            item.FilterUrl = this.CreateFilterUrl(webHelper, x.Id, x.Name);

                            return item;
                        }).ToList();
                    }

                    //remove filter URL
                    //string removeFilterUrl = webHelper.RemoveQueryString(webHelper.GetThisPageUrl(true), QUERYSTRINGPARAM);
                    //removeFilterUrl = ExcludeQueryStringParams(removeFilterUrl, webHelper);
                    //this.RemoveFilterUrl = removeFilterUrl;
                }
                else
                {
                    this.Enabled = false;
                }
            }

            #endregion

            #region Properties
            public bool Enabled { get; set; }

            public IList<FilterBaseItem> NotFilteredItems { get; set; }

            public FilterBaseItem FilteredItem { get; set; }

            public string RemoveFilterUrl { get; set; }

            #endregion
        }

        #endregion
    }
}