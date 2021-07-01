using TaskManagementApp.Application.Interfaces;

namespace TaskManagementApp.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(ITaskRepository taskRepository, IProductRepository productRepository)
        {
            Tasks = taskRepository;
            Products = productRepository;
        }
        
        public ITaskRepository Tasks { get; }
        public IProductRepository Products { get; }
    }
}
