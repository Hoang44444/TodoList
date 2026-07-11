using Microsoft.AspNetCore.Mvc;
using TodoList.DTOs.PriorityDTOs;
using TodoList.Services;

namespace TodoList.Controllers
{
    [ApiController]
    [Route("api/[controller]")]     // -> api/priorities
    public class PrioritiesController : ControllerBase
    {
        private readonly IPriorityService _priorityService;
        public PrioritiesController(IPriorityService priorityService)
        {
            _priorityService = priorityService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePriority([FromBody] CreatePriorityDto createPriorityDto, CancellationToken token)
        {
            await _priorityService.CreatePriorityAsync(createPriorityDto, token);
            return Ok(new { message = "Create Successfully" });
        }

        [HttpPut("{priorityId:int}")]
        public async Task<IActionResult> UpdatePriority(int priorityId, [FromBody] UpdatePriorityDto updatePriorityDto, CancellationToken token)
        {
            await _priorityService.UpdatePriorityAsync(priorityId, updatePriorityDto, token);
            return Ok(new { message = "Update Successfully" });
        }

        [HttpDelete("{priorityId:int}")]
        public async Task<IActionResult> DeletePriority(int priorityId, CancellationToken token)
        {
            await _priorityService.DeletePriorityAsync(priorityId, token);
            return Ok(new { message = "Delete Successfully" });
        }

        [HttpGet("{priorityId:int}")]
        public async Task<IActionResult> GetPriorityById(int priorityId, CancellationToken token)
        {
            var priorityResponse = await _priorityService.GetPriorityByIdAsync(priorityId, token);
            return Ok(priorityResponse);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPriorities(CancellationToken token)
        {
            var priorities = await _priorityService.GetAllPrioritiesAsync(token);
            return Ok(priorities);
        }
    }
}
