namespace TodoList.Web.Models
{
    // Khớp shape JSON mà API trả về ở GET /api/todoitems
    public class TodoItemResponseDto
    {
        public int Id { get; set; }
        public string TodoItemName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<TagResponseDto> Tags { get; set; } = new();
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public int? PriorityId { get; set; }
        public string? PriorityName { get; set; }
        public string ReferenceNote { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class TagResponseDto
    {
        public int? Id { get; set; }
        public string TagName { get; set; } = string.Empty;
    }
}
