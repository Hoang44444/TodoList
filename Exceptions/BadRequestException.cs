namespace TodoList.Exceptions
{
    // Ném khi dữ liệu client gửi lên không hợp lệ (validate, tham chiếu sai...) -> 400
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message) { }
    }
}
