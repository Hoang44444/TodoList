using TodoList.Models.Status;

namespace TodoList.Models.Entities
{
    public class Task : Entity
    {
        public string TaskName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public int PriorityId { get; set; }
        public Priority Priority { get; set; } = null!;
        public string ReferenceNote { get; set; } = string.Empty;
        public TodoStatus Status { get; set; } = TodoStatus.Pending;
    }
}
