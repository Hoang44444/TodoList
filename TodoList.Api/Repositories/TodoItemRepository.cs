using Microsoft.EntityFrameworkCore;
using TodoList.Data;
using TodoList.Models.Entities;

namespace TodoList.Repositories
{
    public class TodoItemRepository : GenericRepository<TodoItem>, ITodoItemRepository
    {
        // _context kế thừa (protected) từ GenericRepository
        public TodoItemRepository(AppDbContext context) : base(context)
        {
        }

        // Nạp kèm Tags để EF theo dõi được quan hệ many-to-many khi update
        public async Task<TodoItem?> GetByIdWithTagsAsync(int id, CancellationToken token)
        {
            return await _context.TodoItems
                .Include(t => t.Tags)
                .FirstOrDefaultAsync(t => t.Id == id, token);
        }

        // Nạp kèm Tags + Priority cho GET (cần PriorityName để trả về response)
        public async Task<TodoItem?> GetByIdWithDetailsAsync(int id, CancellationToken token)
        {
            return await _context.TodoItems
                .Include(t => t.Tags)
                .Include(t => t.Priority)
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id, token);
        }

        public async Task<IEnumerable<TodoItem>> GetAllWithDetailsAsync(CancellationToken token)
        {
            return await _context.TodoItems
                .Include(t => t.Tags)
                .Include(t => t.Priority)
                .AsNoTracking()
                .ToListAsync(token);
        }

        public async Task<bool> AnyByPriorityAsync(int priorityId, CancellationToken token)
        {
            return await _context.TodoItems.AnyAsync(t => t.PriorityId == priorityId, token);
        }
    }
}
