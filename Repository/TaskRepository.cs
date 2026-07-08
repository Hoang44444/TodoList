using TodoList.Data;
using Task = TodoList.Models.Entities.Task;

namespace TodoList.Repository
{
    public class TaskRepository : GenericRepository<Task>, ITaskRepository
    {
        public TaskRepository(AppDbContext context) : base(context)
        {
        }
    }
}
