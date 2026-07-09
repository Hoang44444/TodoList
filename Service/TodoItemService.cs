using TodoList.DTOs;
using TodoList.Models.Entities;
using TodoList.Models.Status;
using TodoList.UnitOfWorks;

namespace TodoList.Service
{
    public class TodoItemService : ITodoItemService
    {
        private readonly IUnitOfWork _uow;

        public TodoItemService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task CreateTodoItemAsync(CreateTodoItemDto createTodoItemDto, CancellationToken token)
        {
            var newItem = new TodoItem
            {
                TodoItemName = createTodoItemDto.TodoItemName,
                Description = createTodoItemDto.TodoItemDescription,
                CreatedAt = DateTime.UtcNow,
                StartDate = createTodoItemDto.StarteDate,
                DueDate = createTodoItemDto.DueDate,
                ReferenceNote = createTodoItemDto.ReferenceNote,
                Status = TodoStatus.Pending,
            };
            _uow.TodoItemRepository.Add(newItem);
            await _uow.SaveChangesAsync(token);
        }
    }
}
