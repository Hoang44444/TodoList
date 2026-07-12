using System.Net.Http.Json;
using TodoList.Web.Models;

namespace TodoList.Web.Services
{
    public class TodoApiService : ITodoApiService
    {
        private readonly HttpClient _http;

        public TodoApiService(HttpClient http)
        {
            _http = http;
        }

        // GetFromJsonAsync tự gọi HTTP + parse JSON sang object
        public async Task<List<TodoItemResponseDto>> GetAllAsync()
            => await _http.GetFromJsonAsync<List<TodoItemResponseDto>>("api/todoitems") ?? new();

        public async Task<TodoItemResponseDto?> GetByIdAsync(int id)
            => await _http.GetFromJsonAsync<TodoItemResponseDto>($"api/todoitems/{id}");

        public async Task DeleteAsync(int id)
            => await _http.DeleteAsync($"api/todoitems/{id}");
    }
}
