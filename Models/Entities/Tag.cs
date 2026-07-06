namespace TodoList.Models.Entities
{
    public class Tag : Entity
    {
        public string TagName { get; set; } = string.Empty;
        public ICollection<Task> Tasks { get; set; } = new List<Task>();
    }
}
