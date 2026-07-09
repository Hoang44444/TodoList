using System.ComponentModel.DataAnnotations;
using TodoList.Models.Status;

namespace TodoList.Models.Entities
{
    public class TodoItem : Entity
    {
        [MaxLength(200)]
        public string TodoItemName { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;

        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public int PriorityId { get; set; }
        public Priority Priority { get; set; } = null!;

        [MaxLength(1000)]
        public string ReferenceNote { get; set; } = string.Empty;
        public TodoStatus Status { get; set; } = TodoStatus.Pending;
    }
}
