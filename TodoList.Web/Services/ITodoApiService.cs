using TodoList.Web.Models;

namespace TodoList.Web.Services
{
    public interface ITodoApiService
    {
        Task<List<TodoItemResponseDto>> GetAllAsync();
        Task<TodoItemResponseDto?> GetByIdAsync(int id);
        Task CreateAsync(CreateTodoItemDto dto);
        Task UpdateAsync(int id, CreateTodoItemDto dto);
        Task UpdateStatusAsync(int id, string status);
        Task DeleteAsync(int id);
        Task<List<TagResponseDto>> GetTagsAsync();
        Task<List<PriorityResponseDto>> GetPrioritiesAsync();
    }
}
