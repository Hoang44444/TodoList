using TodoList.DTOs;

namespace TodoList.Service
{
    public interface ITodoItemService
    {
        Task CreateTodoItemAsync(CreateTodoItemDto createTodoItemDto, CancellationToken token);
    }
}
