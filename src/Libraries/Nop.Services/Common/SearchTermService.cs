using System;
using System.Linq;
using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.Common;
using Nop.Services.Events;
using System.Collections.Generic;
using Nop.Data;

namespace Nop.Services.Common
{
    /// <summary>
    /// Search term service
    /// </summary>
    public partial class SearchTermService : ISearchTermService
    {
        #region Fields

        private readonly IRepository<SearchTerm> _searchTermRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly IDbContext _dbContext;

        #endregion

        #region Ctor

        public SearchTermService(IRepository<SearchTerm> searchTermRepository,
            IEventPublisher eventPublisher,
            IDbContext dbContext)
        {
            this._searchTermRepository = searchTermRepository;
            this._eventPublisher = eventPublisher;
            this._dbContext = dbContext;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Deletes a search term record
        /// </summary>
        /// <param name="searchTerm">Search term</param>
        public virtual void DeleteAddress(SearchTerm searchTerm)
        {
            if (searchTerm == null)
                throw new ArgumentNullException("searchTerm");

            _searchTermRepository.Delete(searchTerm);

            //event notification
            _eventPublisher.EntityDeleted(searchTerm);
        }

        /// <summary>
        /// Gets a search term record by identifier
        /// </summary>
        /// <param name="searchTermId">Search term identifier</param>
        /// <returns>Search term</returns>
        public virtual SearchTerm GetSearchTermById(int searchTermId)
        {
            if (searchTermId == 0)
                return null;

            return _searchTermRepository.GetById(searchTermId);
        }

        /// <summary>
        /// Gets a search term record by keyword
        /// </summary>
        /// <param name="keyword">Search term keyword</param>
        /// <param name="storeId">Store identifier</param>
        /// <returns>Search term</returns>
        public virtual SearchTerm GetSearchTermByKeyword(string keyword, int storeId)
        {
            if (String.IsNullOrEmpty(keyword))
                return null;

            var query = from st in _searchTermRepository.Table
                        where st.Keyword == keyword && st.StoreId == storeId
                        orderby st.Id
                        select st;
            var searchTerm = query.FirstOrDefault();
            return searchTerm;
        }

        ///<param name="getMostCommon">Trae las busquedas mas populares sin importar que keyword venga vacio</param>
        /// <summary>
        /// Retorna un listado de busquedas más comunes dependiendo de la cadena enviada
        /// </summary>
        /// <returns></returns>
        public virtual IList<SearchTerm> GetTemsByKeyword(string keyword, int top, bool getMostCommon = false)
        {
            //Valida que existan llaves para buscar

            if(!getMostCommon && string.IsNullOrWhiteSpace(keyword))
                return new List<SearchTerm>();

            //Si debe traer las mas comunes busquedas y no trae filtro retorna de esta manera
            if (getMostCommon && string.IsNullOrEmpty(keyword))
            {
                return _searchTermRepository.Table
                    .OrderByDescending(s => s.Count)
                    .Take(top)
                    .ToList();
            }
            else
            {
                //reemplaza comillas para evitar errores
                keyword = keyword.Replace("'", " ").Replace("\"", " ");

                //recoorre las llaves y las arega para la consulta
                var keywords = new System.Text.StringBuilder();
                foreach (var item in keyword.Split(new char[] { ' ' }))
                {
                    if (keywords.Length > 0)
                        keywords.Append(" AND ");

                    keywords.AppendFormat("formsof(INFLECTIONAL, \"{0}\")", item);
                }

                var query = string.Format("select top {0} Id, Keyword, StoreId, Count from SearchTerm WHERE contains(Keyword, '{1}') order by count desc", top, keywords);

                return _dbContext.ExecuteStoredProcedureList<SearchTerm>(query, new object[0]);
            }

        }

        /// <summary>
        /// Gets a search term statistics
        /// </summary>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>A list search term report lines</returns>
        public virtual IPagedList<SearchTermReportLine> GetStats(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = (from st in _searchTermRepository.Table
                        group st by st.Keyword into groupedResult
                        select new
                        {
                            Keyword = groupedResult.Key,
                            Count = groupedResult.Sum(o => o.Count)
                        })
                        .OrderByDescending(m => m.Count)
                        .Select(r => new SearchTermReportLine
                        {
                            Keyword = r.Keyword,
                            Count = r.Count
                        });

            var result = new PagedList<SearchTermReportLine>(query, pageIndex, pageSize);
            return result;
        }

        /// <summary>
        /// Inserts a search term record
        /// </summary>
        /// <param name="searchTerm">Search term</param>
        public virtual void InsertSearchTerm(SearchTerm searchTerm)
        {
            if (searchTerm == null)
                throw new ArgumentNullException("searchTerm");

            _searchTermRepository.Insert(searchTerm);

            //event notification
            _eventPublisher.EntityInserted(searchTerm);
        }

        /// <summary>
        /// Updates the search term record
        /// </summary>
        /// <param name="searchTerm">Search term</param>
        public virtual void UpdateSearchTerm(SearchTerm searchTerm)
        {
            if (searchTerm == null)
                throw new ArgumentNullException("searchTerm");

            _searchTermRepository.Update(searchTerm);

            //event notification
            _eventPublisher.EntityUpdated(searchTerm);
        }
        
        #endregion
    }
}