using TodoList.Data;
using TodoList.Models.Entities;

namespace TodoList.Repository
{
    public class PriorityRepository : GenericRepository<Priority>, IPriorityRepository
    {
        public PriorityRepository(AppDbContext context) : base(context) { }
    }
}
