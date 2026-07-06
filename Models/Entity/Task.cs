namespace TodoList.Models
{
    public class Task : Entity
    {
        public string TaskName { get; set; }
        public string Description { get; set; } = string.Empty;
        DateTime StartDate { get; set; }
        DateTime DueDate { get; set; }
        TimeOnly Time { get; set; }
        string ReferenceNote { get; set; } = string.Empty;
    }
}
