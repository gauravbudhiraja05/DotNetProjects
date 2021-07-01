using MediatR;

namespace TaskManagementApp.Application.Products.Commands
{
    public class UpdateProductCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
    }
}
