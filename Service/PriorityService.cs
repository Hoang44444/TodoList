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

        public async Task UpdatePriorityAsync(int priorityId, UpdatePriorityDto updatePriorityDto, CancellationToken token)
        {
            var existingPriority = await _uow.PriorityRepository.GetByIdAsync(priorityId, token);
            if(existingPriority == null)
            {
                throw new Exception($"Priority with ID {priorityId} not found.");
            }
            if(string.IsNullOrWhiteSpace(updatePriorityDto.PriorityName))
            {
                throw new Exception("Priority name cannot be empty.");
            }
            existingPriority.PriorityName = updatePriorityDto.PriorityName;
            existingPriority.UpdatedAt = DateTime.UtcNow;

            _uow.PriorityRepository.Update(existingPriority);
            await _uow.SaveChangesAsync(token);
        }
    }
}
