using TodoList.Data;
using TodoList.Models.Entities;
using TodoList.Repository;

namespace TodoList.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private ITodoItemRepository? _todoItemRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public ITodoItemRepository TodoItemRepository => _todoItemRepository ??= new TodoItemRepository(_context);

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> SaveChangesAsync(CancellationToken token)
        {
            return await _context.SaveChangesAsync(token);
        }
    }
}
