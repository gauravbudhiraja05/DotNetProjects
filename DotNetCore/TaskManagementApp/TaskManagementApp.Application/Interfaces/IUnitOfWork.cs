namespace TaskManagementApp.Application.Interfaces
{
    public interface IUnitOfWork
    {
        ITaskRepository Tasks { get; }
        IProductRepository Products { get; }
    }
}
