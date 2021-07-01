using AutoMapper;
using TaskManagementApp.Application.Products.Commands;
using TaskManagementApp.Application.Products.Dto;
using TaskManagementApp.Core.Entities;

namespace TaskManagementApp.Application.Products.MappingProfiles
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<CreateProductCommand, Product>();
            CreateMap<Product, ProductDto>();
        }
    }
}
