using TodoList.DTOs.TodoItemDTOs;

namespace TodoList.Services
{
    public interface ITodoItemService
    {
        Task CreateTodoItemAsync(CreateTodoItemDto createTodoItemDto, CancellationToken token);
        Task UpdateTodoItemAsync(int todoItemId, UpdateTodoItemDto updateTodoItemDto, CancellationToken token);
        Task DeleteTodoItemAsync(int todoItemId, CancellationToken token);
        Task<TodoItemResponseDto> GetTodoItemByIdAsync(int todoItemId, CancellationToken token);
        Task<IEnumerable<TodoItemResponseDto>> GetAllTodoItemsAsync(CancellationToken token);
    }
}
