using ProductMicroServices.Models;
using System.Collections.Generic;

namespace ProductMicroServices.Repository
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts();

        Product GetProductByID(int ProductId);

        void InsertProduct(Product Product);

        void DeleteProduct(int ProductId);

        void UpdateProduct(Product Product);
    }
}
