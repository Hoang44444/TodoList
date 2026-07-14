using TodoList.Models.Entities;

namespace TodoList.Repositories
{
    public interface ITagRepository : IGenericRepository<Tag>
    {
        // Có tag nào trùng tên chưa? excludeId để bỏ qua chính nó khi update.
        Task<bool> ExistsByNameAsync(string name, int? excludeId, CancellationToken token);
    }
}
