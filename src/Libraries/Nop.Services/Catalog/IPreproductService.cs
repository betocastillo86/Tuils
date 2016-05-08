using Nop.Core;
using Nop.Core.Domain.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Catalog
{
    public interface IPreproductService
    {
        void SavePreproduct(Preproduct product);
        
        /// <summary>
        /// Elimina los preproductos creados para el usuario en el tipo de producto
        /// </summary>
        void RemovePreproductsByCustomerId(int customerId, int productTypeId);

        /// <summary>
        /// Retorna el ultimo preproduct creado de un usuario y por tipo
        /// </summary>
        /// <param name="p"></param>
        /// <param name="productType"></param>
        Preproduct GetByUserAndType(int customerId, int productTypeId);
        
        /// <summary>
        /// Retorna todos los preproductos 
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="productTypeId"></param>
        /// <returns></returns>
        IList<Preproduct> GetAllByUserAndType(int customerId, int productTypeId);

        /// <summary>
        /// Retorna todos los preproductos por la busqueda especifica
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="productTypeId"></param>
        /// <returns></returns>
        IPagedList<Preproduct> GetAllPreproducts(string customerEmail = null, string productName = null, int page = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// Retorna un preproducto por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Preproduct GetById(int id);

        /// <summary>
        /// Elimina el preproducto que se envia
        /// </summary>
        /// <param name="preproduct">contenido del preproduct</param>
        void Delete(Preproduct preproduct);
    }
}
