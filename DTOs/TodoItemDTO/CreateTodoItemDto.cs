using TodoList.Models.Status;

namespace TodoList.DTOs
{
    public class CreateTodoItemDto
    {
        public string TodoItemName { get; set; } = string.Empty;
        public string TodoItemDescription { get; set; } = string.Empty;
        public DateTime? StarteDate { get; set; }
        public DateTime? DueDate { get; set; }
        public string ReferenceNote { get; set; } = string.Empty;
    }
}
