namespace TodoList.Models.Entity
{
    public class Priority : Entity
    {
        public string PriorityName { get; set; } = string.Empty;
        public ICollection<Task> Tasks { get; set; } = null!;
    }
}
