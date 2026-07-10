using TodoList.DTOs.PriorityDTOs;
using TodoList.UnitOfWorks;
using TodoList.Models.Entities;

namespace TodoList.Service
{
    public class PriorityService : IPriorityService
    {
        private readonly IUnitOfWork _uow;
        public PriorityService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task CreatePriorityAsync(CreatePriorityDto createPriorityDto, CancellationToken token)
        {
            var newPriority = new Priority
            {
                PriorityName = createPriorityDto.PriorityName,
                CreatedAt =DateTime.UtcNow
            };
            _uow.PriorityRepository.Add(newPriority);
            await _uow.SaveChangesAsync(token);
        }
    }
}
