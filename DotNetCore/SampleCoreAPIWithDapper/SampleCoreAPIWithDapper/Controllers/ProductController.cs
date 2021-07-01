using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleCoreAPIWithDapper.Model;
using SampleCoreAPIWithDapper.Repository;

namespace SampleCoreAPIWithDapper.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        //[Route("{id}")]
        public ActionResult<Product> GetById(int id)
        {
            var product = _productRepository.GetById(id);
            return Ok(product);
        }
        [HttpPost]
        public ActionResult AddProduct(Product entity)
        {
            _productRepository.AddProduct(entity);
            return Ok(entity);
        }
        [HttpPut("{id}")]
        public ActionResult<Product> Update(Product entity, int id)
        {
            _productRepository.UpdateProduct(entity, id);
            return Ok(entity);
        }
        [HttpDelete("{id}")]
        public ActionResult<Product> Delete(int id)
        {
            _productRepository.RemoveProduct(id);
            return Ok();
        }
    }
}
