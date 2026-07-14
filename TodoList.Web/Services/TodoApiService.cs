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

        public async Task CreateAsync(CreateTodoItemDto dto)
        {
            var res = await _http.PostAsJsonAsync("api/todoitems", dto);
            res.EnsureSuccessStatusCode();
        }

        // PUT api/todoitems/{id} — body trùng shape CreateTodoItemDto (UpdateTodoItemDto bên API)
        public async Task UpdateAsync(int id, CreateTodoItemDto dto)
        {
            var res = await _http.PutAsJsonAsync($"api/todoitems/{id}", dto);
            res.EnsureSuccessStatusCode();
        }

        public async Task UpdateStatusAsync(int id, string status)
        {
            var res = await _http.PutAsJsonAsync($"api/todoitems/{id}/status", new { status });
            res.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int id)
            => await _http.DeleteAsync($"api/todoitems/{id}");

        public async Task<List<TagResponseDto>> GetTagsAsync()
            => await _http.GetFromJsonAsync<List<TagResponseDto>>("api/tags") ?? new();

        public async Task<List<PriorityResponseDto>> GetPrioritiesAsync()
            => await _http.GetFromJsonAsync<List<PriorityResponseDto>>("api/priorities") ?? new();

        // POST api/tags → 201 + object vừa tạo (có id)
        public async Task<TagResponseDto> CreateTagAsync(string name)
        {
            var res = await _http.PostAsJsonAsync("api/tags", new { tagName = name });
            res.EnsureSuccessStatusCode();
            return (await res.Content.ReadFromJsonAsync<TagResponseDto>())!;
        }

        // POST api/priorities → 201 + object vừa tạo (có id)
        public async Task<PriorityResponseDto> CreatePriorityAsync(string name)
        {
            var res = await _http.PostAsJsonAsync("api/priorities", new { priorityName = name });
            res.EnsureSuccessStatusCode();
            return (await res.Content.ReadFromJsonAsync<PriorityResponseDto>())!;
        }

        public async Task DeleteTagAsync(int id)
        {
            var res = await _http.DeleteAsync($"api/tags/{id}");
            res.EnsureSuccessStatusCode();
        }

        public async Task DeletePriorityAsync(int id)
        {
            var res = await _http.DeleteAsync($"api/priorities/{id}");
            res.EnsureSuccessStatusCode();
        }
    }
}
