using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Web.Http.Cors;
using WideWorldImporters.API.Models;
using WideWorldImporters.API.Repository;

namespace WideWorldImporters.API.Controllers
{
    [EnableCors("AllowOrigin", "*", "*")]
    [Route("rest/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet("Products")]
        public ActionResult<Product> GetAll(int id)
        {
            List<Product> productList  = _productRepository.GetAllProducts();
            return Ok(productList);
        }

        [HttpGet("Product")]
        public ActionResult<Product> GetById(int id)
        {
            Product product = _productRepository.GetById(id);
            return Ok(product);
        }


        [HttpPost("Product")]
        public ActionResult Add(Product entity)
        {
            _productRepository.AddProduct(entity);
            return Ok(Newtonsoft.Json.JsonConvert.SerializeObject("Product Added Successfully"));
        }

        [HttpPut("Product")]
        public ActionResult<Product> Update(Product entity, int id)
        {
            _productRepository.UpdateProduct(entity, id);
            return Ok(Newtonsoft.Json.JsonConvert.SerializeObject("Product Updated Successfully"));
        }

        [HttpDelete("Product")]
        public ActionResult<Product> Delete(int id)
        {
            _productRepository.RemoveProduct(id);
            return Ok(Newtonsoft.Json.JsonConvert.SerializeObject("Product Deleted Successfully"));
        }
    }
}
