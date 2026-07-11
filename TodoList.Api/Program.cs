using Microsoft.EntityFrameworkCore;
using TodoList.Data;
using TodoList.Middleware;
using TodoList.UnitOfWorks;
using TodoList.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ITodoItemService, TodoItemService>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<IPriorityService, PriorityService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazor", policy =>
    {
        policy.WithOrigins("https://localhost:7223") // Thay đổi URL này thành URL của ứng dụng Blazor của bạn
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
var app = builder.Build();

// Bắt mọi lỗi sớm nhất -> đặt ngoài cùng pipeline
app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("AllowBlazor");

app.UseAuthorization();

app.MapControllers();

app.Run();
