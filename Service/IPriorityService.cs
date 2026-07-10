using TodoList.DTOs.PriorityDTOs;

namespace TodoList.Service
{
    public interface IPriorityService
    {
        Task CreatePriorityAsync(CreatePriorityDto createPriorityDto, CancellationToken token);
    }
}
