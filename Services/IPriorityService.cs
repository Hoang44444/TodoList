using TodoList.DTOs.PriorityDTOs;

namespace TodoList.Services
{
    public interface IPriorityService
    {
        Task CreatePriorityAsync(CreatePriorityDto createPriorityDto, CancellationToken token);
        Task UpdatePriorityAsync(int priorityId, UpdatePriorityDto updatePriorityDto, CancellationToken token);
        Task DeletePriorityAsync(int priorityId, CancellationToken token);
        Task<PriorityResponseDto?> GetPriorityByIdAsync(int priorityId, CancellationToken token);
        Task<IEnumerable<PriorityResponseDto>> GetAllPrioritiesAsync(CancellationToken token);
    }
}
