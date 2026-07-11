using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.DTOs.TodoItemDTOs;
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
            catch (Exception ex)
            {
                // Tầng 2: lỗi nghiệp vụ (không tìm thấy tag/priority, validate...)
                return NotFound(new { message = ex.Message });
            }
        }

        // PUT /api/todoitems/{id}
        [HttpPut("{todoItemId:int}")]
        public async Task<IActionResult> Update(int todoItemId, [FromBody] UpdateTodoItemDto dto, CancellationToken token)
        {
            try
            {
                await _service.UpdateTodoItemAsync(todoItemId, dto, token);
                return Ok(new { message = "Update Successfully" });
            }
            catch (DbUpdateException ex)
            {
                // Tầng 3: lỗi khi lưu (FK sai, trùng key, DB lỗi...)
                return Problem(
                    title: "Can not save the entity",
                    detail: ex.InnerException?.Message ?? ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                // Tầng 2: lỗi nghiệp vụ (không tìm thấy tag/priority, validate...)
                return NotFound(new { message = ex.Message });
            }
        }

        // DELETE /api/todoitems/{id}
        [HttpDelete("{todoItemId:int}")]
        public async Task<IActionResult> Delete(int todoItemId, CancellationToken token)
        {
            try
            {
                await _service.DeleteTodoItemAsync(todoItemId, token);
                return Ok(new { message = "Delete Successfully" });
            }
            catch (DbUpdateException ex)
            {
                // Tầng 3: lỗi khi lưu (FK sai, ràng buộc DB...)
                return Problem(
                    title: "Can not delete the entity",
                    detail: ex.InnerException?.Message ?? ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                // Tầng 2: lỗi nghiệp vụ (không tìm thấy...)
                return NotFound(new { message = ex.Message });
            }
        }

        // GET /api/todoitems/{id}
        [HttpGet("{todoItemId:int}")]
        public async Task<IActionResult> GetById(int todoItemId, CancellationToken token)
        {
            try
            {
                var todoItem = await _service.GetTodoItemByIdAsync(todoItemId, token);
                return Ok(todoItem);
            }
            catch (Exception ex)
            {
                // Tầng 2: lỗi nghiệp vụ (không tìm thấy...)
                return NotFound(new { message = ex.Message });
            }
        }

        // GET /api/todoitems
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken token)
        {
            var todoItems = await _service.GetAllTodoItemsAsync(token);
            return Ok(todoItems);
        }
    }
}
