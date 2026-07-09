using TodoList.Data;
using TodoList.Models.Entities;
using TodoList.Repository;

namespace TodoList.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly Dictionary<Type, object> _repositories;
        public ITodoItemRepository TodoItemRepository { get; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            _repositories = new Dictionary<Type, object>();
            TodoItemRepository = new TodoItemRepository(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IGenericRepository<T> Repository<T>() where T : Entity
        {
            if(_repositories.TryGetValue(typeof(T), out var repository))
            {
                return (IGenericRepository<T>)repository;
            }
            var newRepository = new GenericRepository<T>(_context);
            _repositories.Add(typeof(T), newRepository);
            return newRepository;
        }

        public async Task<int> SaveChangesAsync(CancellationToken token)
        {
            return await _context.SaveChangesAsync(token);
        }
    }
}
