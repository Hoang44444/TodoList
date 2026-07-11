using Microsoft.AspNetCore.Mvc;
using TodoList.DTOs.TodoItemDTOs;
using TodoList.Services;

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
            await _service.CreateTodoItemAsync(dto, token);
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT /api/todoitems/{id}
        [HttpPut("{todoItemId:int}")]
        public async Task<IActionResult> Update(int todoItemId, [FromBody] UpdateTodoItemDto dto, CancellationToken token)
        {
            await _service.UpdateTodoItemAsync(todoItemId, dto, token);
            return Ok(new { message = "Update Successfully" });
        }

        // DELETE /api/todoitems/{id}
        [HttpDelete("{todoItemId:int}")]
        public async Task<IActionResult> Delete(int todoItemId, CancellationToken token)
        {
            await _service.DeleteTodoItemAsync(todoItemId, token);
            return Ok(new { message = "Delete Successfully" });
        }

        // GET /api/todoitems/{id}
        [HttpGet("{todoItemId:int}")]
        public async Task<IActionResult> GetById(int todoItemId, CancellationToken token)
        {
            var todoItem = await _service.GetTodoItemByIdAsync(todoItemId, token);
            return Ok(todoItem);
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
