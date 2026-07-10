using TodoList.DTOs.TagDTO;

namespace TodoList.Service
{
    public interface ITagService
    {
        Task CreateTagAsync(CreateTagDto createTagDto, CancellationToken token);
    }
}
