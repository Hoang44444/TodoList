using TodoList.DTOs.PriorityDTOs;

namespace TodoList.Service
{
    public interface IPriorityService
    {
        Task CreatePriorityAsync(CreatePriorityDto createPriorityDto, CancellationToken token);
        Task UpdatePriorityAsync(int priorityId, UpdatePriorityDto updatePriorityDto, CancellationToken token);
        Task DeletePriorityAsync(int priorityId, CancellationToken token);
        Task<PriorityResponse?> GetPriorityByIdAsync(int priorityId, CancellationToken token);
        Task<IEnumerable<PriorityResponse>> GetAllPrioritiesAsync(CancellationToken token);
    }
}
