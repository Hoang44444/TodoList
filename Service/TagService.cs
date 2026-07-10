using TodoList.DTOs.TagDTOs;
using TodoList.Models.Entities;
using TodoList.UnitOfWorks;

namespace TodoList.Service
{
    public class TagService : ITagService
    {
        private readonly IUnitOfWork _uow;
        public TagService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task CreateTagAsync(CreateTagDto createTagDto, CancellationToken token)
        {
            var newTag = new Tag
            {
                TagName = createTagDto.TagName,
                CreatedAt = DateTime.UtcNow,
            };

            _uow.TagRepository.Add(newTag);
            await _uow.SaveChangesAsync(token);
        }

        public async Task UpdateTagAsync(int tagId, UpdateTagDto updateTagDto, CancellationToken token)
        {
            var tag = await _uow.TagRepository.GetByIdAsync(tagId, token);
            if (tag == null)
            {
                throw new Exception($"Tag with ID {tagId} not found.");
            }

            tag.TagName = updateTagDto.TagName;
            _uow.TagRepository.Update(tag);
            await _uow.SaveChangesAsync(token);
        }

        public async Task DeleteTagAsync(int tagId, CancellationToken token)
        {
            var tag = await _uow.TagRepository.GetByIdAsync(tagId, token);
            if (tag == null)
            {
                throw new Exception($"Tag with ID {tagId} not found.");
            }
            _uow.TagRepository.Delete(tag);
            await _uow.SaveChangesAsync(token);
        }
    }
}
