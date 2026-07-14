using TodoList.Models.Entities;

namespace TodoList.Repositories
{
    public interface ITodoItemRepository : IGenericRepository<TodoItem>
    {
        Task<TodoItem?> GetByIdWithTagsAsync(int id, CancellationToken token);
        Task<TodoItem?> GetByIdWithDetailsAsync(int id, CancellationToken token);
        Task<IEnumerable<TodoItem>> GetAllWithDetailsAsync(CancellationToken token);

        // Có TodoItem nào đang dùng priority này không? (chặn xóa priority đang được tham chiếu)
        Task<bool> AnyByPriorityAsync(int priorityId, CancellationToken token);
    }
}
