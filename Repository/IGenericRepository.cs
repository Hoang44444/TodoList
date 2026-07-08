using TodoList.Models.Entities;

namespace TodoList.Repository
{
    public interface IGenericRepository<T> where T : Entity
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T?>> GetAllAsync();
    }
}
