using Task = TodoList.Models.Entities.Task;

namespace TodoList.Repository
{
    public interface ITaskRepository : IGenericRepository<Task>
    {
    }
}
