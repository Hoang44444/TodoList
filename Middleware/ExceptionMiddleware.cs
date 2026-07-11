using Microsoft.EntityFrameworkCore;
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
                // Chọn status code + message theo loại exception
                var (statusCode, message) = ex switch
                {
                    NotFoundException => (StatusCodes.Status404NotFound, ex.Message),
                    BadRequestException => (StatusCodes.Status400BadRequest, ex.Message),
                    DbUpdateException dbEx => (StatusCodes.Status500InternalServerError,
                        dbEx.InnerException?.Message ?? dbEx.Message),
                    _ => (StatusCodes.Status500InternalServerError, ex.Message)
                };

                // Lỗi ngoài dự kiến (500) thì ghi log để còn debug
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
