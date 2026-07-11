using TodoList.Data;
using TodoList.Models.Entities;

namespace TodoList.Repositories
{
    public class TagRepository : GenericRepository<Tag>, ITagRepository
    {
        public TagRepository(AppDbContext context) : base(context)
        {
        }
    }
}
