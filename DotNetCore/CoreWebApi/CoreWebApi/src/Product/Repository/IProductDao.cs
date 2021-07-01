using CoreWebApi.src.Product.Model;
using System.Collections.Generic;

namespace CoreWebApi.src.Product.Repository
{
    public interface IProductDao
    {
        List<ProductDto> GetProducts();
        void AddProduct(ProductDto productItem);
        void UpdateProduct(int id, ProductDto productItem);
        void DeleteProduct(int id);
    }
}
