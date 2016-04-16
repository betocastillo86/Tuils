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
                    insertedPreproduct.ProductName = product.ProductName;
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

        public Preproduct GetById(int id)
        {
            return _preproductRepository.GetById(id);
        }

        public void Delete(Preproduct preproduct)
        {
            if (preproduct == null)
                throw new ArgumentNullException("preproduct");

            _preproductRepository.Delete(preproduct);
        }

        public IList<Preproduct> GetAllByUserAndType(int customerId, int productTypeId)
        {
            return _preproductRepository.Table
                .Where(p => p.CustomerId == customerId && p.ProductTypeId == productTypeId)
                .ToList();
        }


        public IPagedList<Preproduct> GetAllPreproducts(string customerEmail = null, string productName = null, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _preproductRepository.Table;

            if (!string.IsNullOrEmpty(customerEmail))
                query = query.Where(p => p.Customer.Email.Contains(customerEmail));

            if (!string.IsNullOrEmpty(productName))
                query = query.Where(p => p.ProductName.Contains(productName));

            query = query.OrderByDescending(c => c.CreatedOnUtc);

            return new PagedList<Preproduct>(query, pageIndex, pageSize);

        }
    }
}
