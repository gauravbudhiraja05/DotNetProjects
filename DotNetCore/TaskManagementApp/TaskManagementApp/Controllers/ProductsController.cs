using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagementApp.Application.Products.Commands;
using TaskManagementApp.Application.Products.Dto;
using TaskManagementApp.Application.Products.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace TaskManagementApp.Api.Controllers
{
    [ApiController]
    [Route("rest/products")]
    public class ProductsController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();


        [HttpPost("SaveProduct")]
        public async Task<ActionResult<int>> CreateProduct(CreateProductCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet("GetAllProducts")]
        public async Task<ActionResult<List<ProductDto>>> GetAllProducts()
        {
            return await Mediator.Send(new GetAllProductsQuery());
        }

        [HttpGet("GetProductById")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            return await Mediator.Send(new GetProductByIdQuery { Id = id });
        }

        [HttpPut("UpdateProduct")]
        public async Task<ActionResult<int>> UpdateProduct(UpdateProductCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("DeleteProductById")]
        public async Task<ActionResult<int>> DeleteProductById(int id)
        {
            return await Mediator.Send(new DeleteProductCommand { Id = id });
        }
    }
}
