using System.ComponentModel.DataAnnotations;
using TodoList.Models.Status;

namespace TodoList.DTOs.TodoItemDTOs
{
    public class UpdateTodoItemDto
    {
        [MaxLength(200)]
        public string TodoItemName { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string TodoItemDescription { get; set; } = string.Empty;
        public List<int>? TagIds { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public int? PriorityId { get; set; }

        [MaxLength(1000)]
        public string ReferenceNote { get; set; } = string.Empty;
        public TodoStatus? UpdateStatus { get; set; }
    }
}
