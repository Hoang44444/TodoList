using Microsoft.EntityFrameworkCore;
using TodoList.Models.Entities;

namespace TodoList.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){ }

        public DbSet<TodoItem> TodoItems { get; set; } = null!;
        public DbSet<Priority> Priorities { get; set; } = null!;
        public DbSet<Tag> Tags { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // TodoItem -> Priority (one-to-many): chặn xóa Priority khi còn TodoItem đang dùng
            modelBuilder.Entity<TodoItem>()
                .HasOne(t => t.Priority)
                .WithMany(p => p.TodoItems)
                .OnDelete(DeleteBehavior.Restrict);

            // Không cho trùng tên Tag
            modelBuilder.Entity<Tag>()
                .HasIndex(t => t.TagName)
                .IsUnique();
        }
    }
}
