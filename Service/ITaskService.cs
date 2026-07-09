using TodoList.DTOs;

namespace TodoList.Service
{
    public interface ITaskService
    {
        Task CreateTaskAsync(CreateTaskDto taskDto, CancellationToken token);
    }
}
