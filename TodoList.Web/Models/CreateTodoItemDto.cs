namespace TodoList.Web.Models
{
    // Khớp CreateTodoItemDto bên API (POST /api/todoitems)
    public class CreateTodoItemDto
    {
        public string TodoItemName { get; set; } = "";
        public string TodoItemDescription { get; set; } = "";
        public List<int>? TagIds { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public int? PriorityId { get; set; }
        public string ReferenceNote { get; set; } = "";
    }
}
