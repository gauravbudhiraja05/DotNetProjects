using CoreWebApi.src.Product.Model;
using System.Collections.Generic;

namespace CoreWebApi.src.Product.Af
{
    public interface IProductAf
    {
        List<ProductDto> GetProducts();
        void AddProduct(ProductDto productItem);
        void UpdateProduct(int id, ProductDto productItem);
        void DeleteProduct(int id);
    }
}
