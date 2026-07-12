using System.ComponentModel.DataAnnotations;

namespace TodoList.Models.Entities
{
    public class Tag : Entity
    {
        [MaxLength(50)]
        public string TagName { get; set; } = string.Empty;
        public ICollection<TodoItem> TodoItems { get; set; } = new List<TodoItem>();
    }
}
