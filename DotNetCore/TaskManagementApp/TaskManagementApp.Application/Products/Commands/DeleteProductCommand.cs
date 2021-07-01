using MediatR;

namespace TaskManagementApp.Application.Products.Commands
{
    public class DeleteProductCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}
