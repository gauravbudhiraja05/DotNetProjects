using System.Collections.Generic;
using WideWorldImporters.API.Models;

namespace WideWorldImporters.API.Repository
{
    public interface IProductRepository
    {
        Product GetById(int id);
        void AddProduct(Product entity);
        void UpdateProduct(Product entity, int id);
        void RemoveProduct(int id);
        List<Product> GetAllProducts();
    }
}
