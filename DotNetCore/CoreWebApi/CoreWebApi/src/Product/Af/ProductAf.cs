using CoreWebApi.src.Product.Model;
using CoreWebApi.src.Product.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebApi.src.Product.Af
{
    public class ProductAf : IProductAf
    {
        private IProductService _productsService;
        public ProductAf(IProductService productsService)
        {
            _productsService = productsService;
        }
        public List<ProductDto> GetProducts()
        {
            return _productsService.GetProducts();
        }
        public void AddProduct(ProductDto productItem)
        {
            _productsService.AddProduct(productItem);
        }
        public void UpdateProduct(int id, ProductDto productItem)
        {
            _productsService.UpdateProduct(id,productItem);
        }
        public void DeleteProduct(int id)
        {
            _productsService.DeleteProduct(id);
        }
    }
}
