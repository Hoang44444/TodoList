namespace TodoList.Models.Entity
{
    public class Tag : Entity
    {
        string TagName { get; set; }
        public ICollection<Task> Tasks { get; set; } = new List<Task>();
    }
}
