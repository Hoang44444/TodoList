using TodoList.Exceptions;

namespace TodoList.Middleware
{
    // Bắt mọi exception nổi lên từ Controller/Service, map ra status code + JSON đồng nhất
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // cho request chạy tiếp xuống controller
            }
            catch (Exception ex)
            {
                // Chọn status code + message theo loại exception.
                // Chỉ các lỗi "do client" mới lộ message; mọi lỗi ngoài dự kiến (500)
                // trả message chung để không rò rỉ chi tiết SQL/stack ra ngoài.
                var (statusCode, message) = ex switch
                {
                    NotFoundException => (StatusCodes.Status404NotFound, ex.Message),
                    BadRequestException => (StatusCodes.Status400BadRequest, ex.Message),
                    ConflictException => (StatusCodes.Status409Conflict, ex.Message),
                    _ => (StatusCodes.Status500InternalServerError,
                        "Đã xảy ra lỗi phía máy chủ. Vui lòng thử lại sau.")
                };

                // Lỗi ngoài dự kiến (500) thì ghi log đầy đủ để còn debug
                if (statusCode == StatusCodes.Status500InternalServerError)
                {
                    _logger.LogError(ex, "Unhandled exception");
                }

                context.Response.StatusCode = statusCode;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(new { message });
            }
        }
    }
}
