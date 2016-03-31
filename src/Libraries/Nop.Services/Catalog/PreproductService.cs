using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Catalog
{
    public class PreproductService : IPreproductService
    {
        #region Fields
        private readonly IRepository<Preproduct> _preproductRepository;
        #endregion

        public PreproductService(IRepository<Preproduct> preproductRepository)
        {
            _preproductRepository = preproductRepository;
        }
        
        public void SavePreproduct(Preproduct product)
        {
            if (product.Id > 0)
            {
                //Valida que exista y este asignado al usuario
                var insertedPreproduct = _preproductRepository.Table
                    .FirstOrDefault(p => p.CustomerId == product.CustomerId && p.Id == product.Id);

                if (insertedPreproduct != null)
                {
                    insertedPreproduct.JsonObject = product.JsonObject;
                    insertedPreproduct.UpdatedOnUtc = DateTime.UtcNow;
                    //Crea el producto nuevamente
                    _preproductRepository.Update(insertedPreproduct);
                }
                else
                {
                    throw new NopException("No puede guardar un preproducto de otro usuario");
                }
            }
            else
            {
                //Crea el preproducto
                product.CreatedOnUtc = DateTime.UtcNow;
                _preproductRepository.Insert(product);
            }
        }

        /// <summary>
        /// Elimina los preproductos creados para el usuario en el tipo de producto
        /// </summary>
        public void RemovePreproductsByCustomerId(int customerId, int productTypeId)
        {
            var preproducts = _preproductRepository.Table.Where(p => p.CustomerId == customerId && p.ProductTypeId == productTypeId);
            _preproductRepository.Delete(preproducts);
        }

        /// <summary>
        /// Retorna un preproducto por usuario y tipo de producto
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="productTypeId"></param>
        /// <returns></returns>
        public Preproduct GetByUserAndType(int customerId, int productTypeId)
        {
            var preproduct = _preproductRepository.Table
                .OrderByDescending(p => p.Id)
                .FirstOrDefault(p => p.CustomerId == customerId && p.ProductTypeId == productTypeId);
            return preproduct;
        }
    }
}
