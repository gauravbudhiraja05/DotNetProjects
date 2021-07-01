using CoreWebApi.src.Product.Af;
using CoreWebApi.src.Product.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace CoreWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
         private IProductAf _productAf;

        public ProductsController(IProductAf productAf)
        {
            _productAf = productAf;
        }

        [HttpGet("/api/products")]
        public ActionResult<List<ProductDto>> GetProducts()
        {
            return _productAf.GetProducts();
        }

        [HttpPost("/api/products")]
        public ActionResult<List<ProductDto>> AddProduct(ProductDto product)
        {
            _productAf.AddProduct(product);
            return GetProducts();
        }

        [HttpPut("/api/products/{id}")]
        public ActionResult<List<ProductDto>> UpdateProduct(int id, ProductDto product)
        {
            _productAf.UpdateProduct(id, product);
            return GetProducts();
        }

        [HttpDelete("/api/products/{id}")]
        public ActionResult<List<ProductDto>> DeleteProduct(int id)
        {
            _productAf.DeleteProduct(id);
             return GetProducts();
        }
    }

}
