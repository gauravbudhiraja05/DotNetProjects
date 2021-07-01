using MediatR;
using System.Collections.Generic;
using TaskManagementApp.Application.Products.Dto;

namespace TaskManagementApp.Application.Products.Queries
{
    public class GetAllProductsQuery : IRequest<List<ProductDto>>
    {
    }
}
