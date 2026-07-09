using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.DTOs;
using TodoList.Service;

namespace TodoList.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoItemService _service;

        public TodoItemsController(ITodoItemService service)
        {
            _service = service;
        }

        // POST /api/todoitems
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTodoItemDto dto, CancellationToken token)
        {
            // Tầng 1 (input không hợp lệ) đã được [ApiController] tự chặn và trả 400 trước khi vào đây.
            try
            {
                await _service.CreateTodoItemAsync(dto, token);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (DbUpdateException ex)
            {
                // Tầng 3: lỗi khi lưu (FK sai, trùng key, DB lỗi...)
                return Problem(
                    title: "Can not save the entity",
                    detail: ex.InnerException?.Message ?? ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
        }
    }
}
