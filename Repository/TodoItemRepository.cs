using TodoList.Data;
using TodoList.Models.Entities;

namespace TodoList.Repository
{
    public class TodoItemRepository : GenericRepository<TodoItem>, ITodoItemRepository
    {
        public TodoItemRepository(AppDbContext context) : base(context)
        {
        }
    }
}
