namespace TodoList.Exceptions
{
    // Ném khi tài nguyên chính (theo id trên URL) không tồn tại -> 404
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
    }
}
