using Microsoft.EntityFrameworkCore;
using TodoList.Models.Entities;
using Task = TodoList.Models.Entities.Task;

namespace TodoList.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){ }

        public DbSet<Task> Tasks { get; set; } = null!;
        public DbSet<Priority> Priorities { get; set; } = null!;
        public DbSet<Tag> Tags { get; set; } = null!;
    }
}
