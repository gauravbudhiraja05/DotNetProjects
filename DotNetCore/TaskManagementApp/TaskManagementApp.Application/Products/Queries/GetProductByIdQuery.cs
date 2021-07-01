using MediatR;
using TaskManagementApp.Application.Products.Dto;

namespace TaskManagementApp.Application.Products.Queries
{
    public class GetProductByIdQuery : IRequest<ProductDto>
    {
        public int Id { get; set; }
    }
}
