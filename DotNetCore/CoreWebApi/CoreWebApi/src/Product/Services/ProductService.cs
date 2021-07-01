using CoreWebApi.src.Product.Model;
using CoreWebApi.src.Product.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebApi.src.Product.Services
{
    public class ProductService : IProductService
    {
        private IProductDao _productDao;
        public ProductService(IProductDao productDao)
        {
            _productDao = productDao;
        }

        public List<ProductDto> GetProducts()
        {
            return _productDao.GetProducts();
        }
        public void AddProduct(ProductDto productItem)
        {
            _productDao.AddProduct(productItem);
        }
        public void UpdateProduct(int id, ProductDto productItem)
        {
            _productDao.UpdateProduct(id, productItem);
        }
        public void DeleteProduct(int id)
        {
            _productDao.DeleteProduct(id);
        }
    }
}
