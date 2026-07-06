namespace TodoList.Models.Entity
{
    public class Priority : Entity
    {
        string PriorityName { get; set; } = string.Empty;
        ICollection<Task> Tasks { get; set; } = new List<Task>();
    }
}
