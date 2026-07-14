namespace TodoList.Exceptions
{
    // Ném khi thao tác hợp lệ về cú pháp nhưng xung đột với trạng thái dữ liệu hiện tại
    // (trùng tên có ràng buộc unique, xóa bản ghi đang được tham chiếu...) -> 409
    public class ConflictException : Exception
    {
        public ConflictException(string message) : base(message) { }
    }
}
