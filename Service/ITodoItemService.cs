using TodoList.DTOs.TodoItemDTOs;

namespace TodoList.Service
{
    public interface ITodoItemService
    {
        Task CreateTodoItemAsync(CreateTodoItemDto createTodoItemDto, CancellationToken token);
        Task UpdateTodoItemAsync(int todoItemId, UpdateTodoItemDto updateTodoItemDto, CancellationToken token);
    }
}
