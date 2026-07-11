using TodoList.Models.Entities;
using TodoList.Repositories;

namespace TodoList.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken token);

        public ITodoItemRepository TodoItemRepository { get; }
        public ITagRepository TagRepository { get; }
        public IPriorityRepository PriorityRepository { get; }
    }
}
