using TodoList.DTOs.TagDTOs;

namespace TodoList.Services
{
    public interface ITagService
    {
        Task<TagResponseDto> CreateTagAsync(CreateTagDto createTagDto, CancellationToken token);
        Task UpdateTagAsync(int tagId, UpdateTagDto updateTagDto, CancellationToken token);
        Task DeleteTagAsync(int tagId, CancellationToken token);
        Task<TagResponseDto?> GetTagByIdAsync(int tagId, CancellationToken token);
        Task<IEnumerable<TagResponseDto>> GetAllTagsAsync(CancellationToken token);
    }
}
