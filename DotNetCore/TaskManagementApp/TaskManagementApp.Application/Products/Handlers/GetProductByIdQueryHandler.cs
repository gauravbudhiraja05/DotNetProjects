using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TaskManagementApp.Application.Interfaces;
using TaskManagementApp.Application.Products.Dto;
using TaskManagementApp.Application.Products.Queries;

namespace TaskManagementApp.Application.Products.Handlers
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Tasks.Get(request.Id);
            return _mapper.Map<ProductDto>(result);
        }
    }
}
