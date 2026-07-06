namespace TodoList.Models.Entity
{
    public class Priority : Entity
    {
        public string PriorityName { get; set; } = string.Empty;
        ICollection<Task> Tasks { get; set; } = null!;
    }
}
