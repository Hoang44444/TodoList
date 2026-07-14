using System.Net;
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
            await EnsureSuccessAsync(res);
        }

        // PUT api/todoitems/{id} — body trùng shape CreateTodoItemDto (UpdateTodoItemDto bên API)
        public async Task UpdateAsync(int id, CreateTodoItemDto dto)
        {
            var res = await _http.PutAsJsonAsync($"api/todoitems/{id}", dto);
            await EnsureSuccessAsync(res);
        }

        public async Task UpdateStatusAsync(int id, string status)
        {
            var res = await _http.PutAsJsonAsync($"api/todoitems/{id}/status", new { status });
            await EnsureSuccessAsync(res);
        }

        public async Task DeleteAsync(int id)
        {
            var res = await _http.DeleteAsync($"api/todoitems/{id}");
            await EnsureSuccessAsync(res);
        }

        public async Task<List<TagResponseDto>> GetTagsAsync()
            => await _http.GetFromJsonAsync<List<TagResponseDto>>("api/tags") ?? new();

        public async Task<List<PriorityResponseDto>> GetPrioritiesAsync()
            => await _http.GetFromJsonAsync<List<PriorityResponseDto>>("api/priorities") ?? new();

        // POST api/tags → 201 + object vừa tạo (có id)
        public async Task<TagResponseDto> CreateTagAsync(string name)
        {
            var res = await _http.PostAsJsonAsync("api/tags", new { tagName = name });
            await EnsureSuccessAsync(res);
            return (await res.Content.ReadFromJsonAsync<TagResponseDto>())!;
        }

        // POST api/priorities → 201 + object vừa tạo (có id)
        public async Task<PriorityResponseDto> CreatePriorityAsync(string name)
        {
            var res = await _http.PostAsJsonAsync("api/priorities", new { priorityName = name });
            await EnsureSuccessAsync(res);
            return (await res.Content.ReadFromJsonAsync<PriorityResponseDto>())!;
        }

        public async Task DeleteTagAsync(int id)
        {
            var res = await _http.DeleteAsync($"api/tags/{id}");
            await EnsureSuccessAsync(res);
        }

        public async Task DeletePriorityAsync(int id)
        {
            var res = await _http.DeleteAsync($"api/priorities/{id}");
            await EnsureSuccessAsync(res);
        }

        // API trả lỗi dạng { "message": "..." }. Khi status không thành công,
        // đọc message đó ra để hiển thị cho user thay vì text mặc định "... 409 (Conflict)".
        private static async Task EnsureSuccessAsync(HttpResponseMessage res)
        {
            if (res.IsSuccessStatusCode) return;

            string? message = null;
            try
            {
                var error = await res.Content.ReadFromJsonAsync<ApiError>();
                message = error?.Message;
            }
            catch
            {
                // Body không phải JSON chuẩn — bỏ qua, dùng message dự phòng bên dưới.
            }

            throw new ApiException(
                string.IsNullOrWhiteSpace(message) ? $"Lỗi máy chủ ({(int)res.StatusCode})" : message,
                res.StatusCode);
        }

        private sealed record ApiError(string? Message);
    }

    // Ngoại lệ mang message thân thiện lấy từ body API + status code (để page hiển thị).
    public class ApiException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public ApiException(string message, HttpStatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
