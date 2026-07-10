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
            var tags = await _uow.TagRepository.GetAllAsync(token);
            foreach (var tagId in createTodoItemDto.TagIds ?? new List<int>())
            {
                if (!tags.Any(t => t.Id == tagId))
                {
                    throw new Exception($"Tag with Id {tagId} not found");
                }
            }

            if(createTodoItemDto.PriorityId.HasValue)
            {
                var priority = await _uow.PriorityRepository.GetByIdAsync(createTodoItemDto.PriorityId.Value, token);
                if (priority == null)
                {
                    throw new Exception("Priority not found");
                }
            }

            var newTodoItem = new TodoItem
            {
                TodoItemName = createTodoItemDto.TodoItemName,
                Description = createTodoItemDto.TodoItemDescription,
                Tags = tags.Where(t => createTodoItemDto.TagIds?.Contains(t.Id) ?? false).ToList(),
                StartDate = createTodoItemDto.StartDate,
                DueDate = createTodoItemDto.DueDate,
                PriorityId = createTodoItemDto.PriorityId,
                ReferenceNote = createTodoItemDto.ReferenceNote,
                Status = TodoStatus.Pending
            };
            _uow.TodoItemRepository.Add(newTodoItem);
            await _uow.SaveChangesAsync(token);

        }
    }
}
