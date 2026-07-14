using Microsoft.EntityFrameworkCore;
using TodoList.Data;
using TodoList.Models.Entities;

namespace TodoList.Repositories
{
    public class TagRepository : GenericRepository<Tag>, ITagRepository
    {
        public TagRepository(AppDbContext context) : base(context)
        {
        }

        // So sánh bằng ở SQL Server dùng collation không phân biệt hoa/thường (CI),
        // khớp với unique index trên TagName.
        public async Task<bool> ExistsByNameAsync(string name, int? excludeId, CancellationToken token)
        {
            return await _dbSet.AnyAsync(
                t => t.TagName == name && (!excludeId.HasValue || t.Id != excludeId.Value),
                token);
        }
    }
}
