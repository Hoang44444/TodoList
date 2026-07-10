using TodoList.DTOs.TodoItemDTOs;
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

            if(createTodoItemDto.StartDate.HasValue && createTodoItemDto.DueDate.HasValue)
            {
                if (createTodoItemDto.StartDate > createTodoItemDto.DueDate)
                {
                    throw new Exception("Start date cannot be later than due date");
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
                Status = TodoStatus.Pending,
                CreatedAt = DateTime.UtcNow
            };
            _uow.TodoItemRepository.Add(newTodoItem);
            await _uow.SaveChangesAsync(token);

        }

        public async Task UpdateTodoItemAsync(int todoItemId, UpdateTodoItemDto updateTodoItemDto, CancellationToken token)
        {
            var todoItem = await _uow.TodoItemRepository.GetByIdWithTagsAsync(todoItemId, token);
            if (todoItem == null)
            {
                throw new Exception($"TodoItem with id: {todoItemId} not found");
            }

            if (string.IsNullOrWhiteSpace(updateTodoItemDto.TodoItemName))
            {
                throw new Exception("TodoItem name cannot be empty");
            }

            var tags = await _uow.TagRepository.GetAllAsync(token);
            foreach (var tagId in updateTodoItemDto.TagIds ?? new List<int>())
            {
                if (!tags.Any(t => t.Id == tagId))
                {
                    throw new Exception($"Tag with Id {tagId} not found");
                }
            }

            if (updateTodoItemDto.PriorityId.HasValue)
            {
                var priority = await _uow.PriorityRepository.GetByIdAsync(updateTodoItemDto.PriorityId.Value, token);
                if (priority == null)
                {
                    throw new Exception("Priority not found");
                }
            }

            if (updateTodoItemDto.StartDate.HasValue && updateTodoItemDto.DueDate.HasValue)
            {
                if (updateTodoItemDto.StartDate > updateTodoItemDto.DueDate)
                {
                    throw new Exception("Start date cannot be later than due date");
                }
            }

            todoItem.TodoItemName = updateTodoItemDto.TodoItemName;
            todoItem.Description = updateTodoItemDto.TodoItemDescription;
            todoItem.Tags = tags.Where(t => updateTodoItemDto.TagIds?.Contains(t.Id) ?? false).ToList();
            todoItem.StartDate = updateTodoItemDto.StartDate;
            todoItem.DueDate = updateTodoItemDto.DueDate;
            todoItem.PriorityId = updateTodoItemDto.PriorityId;
            todoItem.ReferenceNote = updateTodoItemDto.ReferenceNote;
            todoItem.UpdatedAt = DateTime.UtcNow;

            _uow.TodoItemRepository.Update(todoItem);
            await _uow.SaveChangesAsync(token);
        }
    }
}
