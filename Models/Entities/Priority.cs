using System.ComponentModel.DataAnnotations;

namespace TodoList.Models.Entities
{
    public class Priority : Entity
    {
        [MaxLength(50)]
        public string PriorityName { get; set; } = string.Empty;
        public ICollection<TodoItem> TodoItems { get; set; } = new List<TodoItem>();
    }
}
