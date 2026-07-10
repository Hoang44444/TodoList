using TodoList.DTOs.TagDTO;
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
    }
}
