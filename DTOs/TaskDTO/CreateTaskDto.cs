using TodoList.Models.Status;

namespace TodoList.DTOs
{
    public class CreateTaskDto
    {
        string TaskName { get; set; } = string.Empty;
        string TaskDescription { get; set; } = string.Empty;
        DateTime? StartedDate { get; set; }
        DateTime? DueDate { get; set; }
        string ReferencesNote { get; set; } = string.Empty;
        TodoStatus Status { get; set; } = TodoStatus.Pending;
    }
}
