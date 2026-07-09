using TodoList.Data;
using TodoList.Models.Entities;

namespace TodoList.Repository
{
    public class TagRepository : GenericRepository<Tag>, ITagRepository
    {
        public TagRepository(AppDbContext context) : base(context)
        {
        }
    }
}
