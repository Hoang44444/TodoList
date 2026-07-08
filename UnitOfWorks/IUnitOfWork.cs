using TodoList.Models.Entities;
using TodoList.Repository;

namespace TodoList.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<T> Repository<T>() where T : Entity;
        Task<int> SaveChangesAsync(CancellationToken token);
    }
}
