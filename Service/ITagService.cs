using TodoList.DTOs.TagDTOs;

namespace TodoList.Service
{
    public interface ITagService
    {
        Task CreateTagAsync(CreateTagDto createTagDto, CancellationToken token);
        Task UpdateTagAsync(int tagId, UpdateTagDto updateTagDto, CancellationToken token);
    }
}
