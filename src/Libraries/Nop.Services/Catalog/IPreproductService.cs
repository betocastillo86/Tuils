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
    }
}
