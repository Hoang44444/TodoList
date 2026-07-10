using Microsoft.EntityFrameworkCore;
using TodoList.Data;
using TodoList.Models.Entities;

namespace TodoList.Repository
{
    public class TodoItemRepository : GenericRepository<TodoItem>, ITodoItemRepository
    {
        private readonly AppDbContext _context;
        public TodoItemRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        // Nạp kèm Tags để EF theo dõi được quan hệ many-to-many khi update
        public async Task<TodoItem?> GetByIdWithTagsAsync(int id, CancellationToken token)
        {
            return await _context.TodoItems
                .Include(t => t.Tags)
                .FirstOrDefaultAsync(t => t.Id == id, token);
        }
    }
}
