using TodoList.DTOs.TodoItemDTOs;
using TodoList.Models.Status;

namespace TodoList.Services
{
    public interface ITodoItemService
    {
        Task<TodoItemResponseDto> CreateTodoItemAsync(CreateTodoItemDto createTodoItemDto, CancellationToken token);
        Task UpdateTodoItemAsync(int todoItemId, UpdateTodoItemDto updateTodoItemDto, CancellationToken token);
        Task UpdateStatusAsync(int todoItemId, TodoStatus status, CancellationToken token);
        Task DeleteTodoItemAsync(int todoItemId, CancellationToken token);
        Task<TodoItemResponseDto> GetTodoItemByIdAsync(int todoItemId, CancellationToken token);
        Task<IEnumerable<TodoItemResponseDto>> GetAllTodoItemsAsync(CancellationToken token);
    }
}
