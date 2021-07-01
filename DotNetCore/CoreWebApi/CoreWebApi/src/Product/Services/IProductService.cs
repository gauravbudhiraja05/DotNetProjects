using CoreWebApi.src.Product.Model;
using System.Collections.Generic;

namespace CoreWebApi.src.Product.Services
{
    public interface IProductService
    {
        List<ProductDto> GetProducts();
        void AddProduct(ProductDto productItem);
        void UpdateProduct(int id, ProductDto productItem);
        void DeleteProduct(int id);
    }
}
