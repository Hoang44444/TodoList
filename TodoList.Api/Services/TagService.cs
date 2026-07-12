using TodoList.DTOs.TagDTOs;
using TodoList.Exceptions;
using TodoList.Models.Entities;
using TodoList.UnitOfWorks;

namespace TodoList.Services
{
    public class TagService : ITagService
    {
        private readonly IUnitOfWork _uow;
        public TagService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<TagResponseDto> CreateTagAsync(CreateTagDto createTagDto, CancellationToken token)
        {
            var newTag = new Tag
            {
                TagName = createTagDto.TagName,
                CreatedAt = DateTime.UtcNow,
            };

            _uow.TagRepository.Add(newTag);
            await _uow.SaveChangesAsync(token);

            return new TagResponseDto
            {
                Id = newTag.Id,
                TagName = newTag.TagName
            };
        }

        public async Task UpdateTagAsync(int tagId, UpdateTagDto updateTagDto, CancellationToken token)
        {
            var tag = await _uow.TagRepository.GetByIdAsync(tagId, token);
            if (tag == null)
            {
                throw new NotFoundException($"Tag with ID {tagId} not found.");
            }

            tag.TagName = updateTagDto.TagName;
            tag.UpdatedAt = DateTime.UtcNow;
            _uow.TagRepository.Update(tag);
            await _uow.SaveChangesAsync(token);
        }

        public async Task DeleteTagAsync(int tagId, CancellationToken token)
        {
            var tag = await _uow.TagRepository.GetByIdAsync(tagId, token);
            if (tag == null)
            {
                throw new NotFoundException($"Tag with ID {tagId} not found.");
            }
            _uow.TagRepository.Delete(tag);
            await _uow.SaveChangesAsync(token);
        }

        public async Task<TagResponseDto?> GetTagByIdAsync(int tagId, CancellationToken token)
        {
            var tag = await _uow.TagRepository.GetByIdAsync(tagId, token);
            if (tag == null)
            {
                throw new NotFoundException($"Tag with ID {tagId} not found.");
            }

            return new TagResponseDto
            {
                Id = tag.Id,
                TagName = tag.TagName
            };
        }

        public async Task<IEnumerable<TagResponseDto>> GetAllTagsAsync(CancellationToken token)
        {
            var tags = await _uow.TagRepository.GetAllAsync(token);
            return tags.Select(tag => new TagResponseDto
            {
                Id = tag.Id,
                TagName = tag.TagName
            });
        }
    }
}
