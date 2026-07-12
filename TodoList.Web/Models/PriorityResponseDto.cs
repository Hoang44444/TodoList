namespace TodoList.Web.Models
{
    // Khớp PriorityResponseDto bên API (GET /api/priorities)
    public class PriorityResponseDto
    {
        public int? Id { get; set; }
        public string PriorityName { get; set; } = "";
    }
}
