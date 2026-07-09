using System.ComponentModel.DataAnnotations;
using TodoList.Models.Status;

namespace TodoList.DTOs
{
    public class CreateTodoItemDto
    {
        [Required(ErrorMessage = "Task name can not be empty")]
        [MaxLength(200)]
        public string TodoItemName { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string TodoItemDescription { get; set; } = string.Empty;
        public List<int>? TagIds { get; set; }

        public DateTime? StarteDate { get; set; }
        public DateTime? DueDate { get; set; }
        public int? PriorityId { get; set; }

        [MaxLength(1000)]
        public string ReferenceNote { get; set; } = string.Empty;
    }
}
