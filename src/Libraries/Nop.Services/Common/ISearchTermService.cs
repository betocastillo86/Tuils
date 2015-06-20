using Nop.Core;
using Nop.Core.Domain.Common;
using System.Collections.Generic;

namespace Nop.Services.Common
{
    /// <summary>
    /// Search term service interafce
    /// </summary>
    public partial interface ISearchTermService
    {
        /// <summary>
        /// Deletes a search term record
        /// </summary>
        /// <param name="searchTerm">Search term</param>
        void DeleteAddress(SearchTerm searchTerm);

        /// <summary>
        /// Gets a search term record by identifier
        /// </summary>
        /// <param name="searchTermId">Search term identifier</param>
        /// <returns>Search term</returns>
        SearchTerm GetSearchTermById(int searchTermId);

        /// <summary>
        /// Gets a search term record by keyword
        /// </summary>
        /// <param name="keyword">Search term keyword</param>
        /// <param name="storeId">Store identifier</param>
        /// <returns>Search term</returns>
        SearchTerm GetSearchTermByKeyword(string keyword, int storeId);


        /// <summary>
        /// Busca todas las coincidencias de la cadena envíada
        /// </summary>
        /// <param name="keyword">llaves por las que el usuario busca</param>
        /// <param name="top">numero maximo de resultados</param>
        /// <returns></returns>
        IList<SearchTerm> GetTemsByKeyword(string keyword, int top);

        /// <summary>
        /// Gets a search term statistics
        /// </summary>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>A list search term report lines</returns>
        IPagedList<SearchTermReportLine> GetStats(int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// Inserts a search term record
        /// </summary>
        /// <param name="searchTerm">Search term</param>
        void InsertSearchTerm(SearchTerm searchTerm);

        /// <summary>
        /// Updates the search term record
        /// </summary>
        /// <param name="searchTerm">Search term</param>
         void UpdateSearchTerm(SearchTerm searchTerm);
    }
}