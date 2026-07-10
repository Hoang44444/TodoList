using Microsoft.AspNetCore.Mvc;
using TodoList.DTOs.PriorityDTOs;
using TodoList.Service;

namespace TodoList.Controllers
{
    [ApiController]
    [Route("api/[controller]")]     // -> api/priority
    public class PriorityController : ControllerBase
    {
        private readonly IPriorityService _priorityService;
        public PriorityController(IPriorityService priorityService)
        {
            _priorityService = priorityService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePriority([FromBody] CreatePriorityDto createPriorityDto, CancellationToken token)
        {
            try
            {
                await _priorityService.CreatePriorityAsync(createPriorityDto, token);
                return Ok(new { message = "Create Successfully" });
            }
            catch (Exception ex)
            {
                // Tầng 2: lỗi nghiệp vụ (không tìm thấy, validate...)
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{priorityId:int}")]
        public async Task<IActionResult> UpdatePriority(int priorityId, [FromBody] UpdatePriorityDto updatePriorityDto, CancellationToken token)
        {
            try
            {
                await _priorityService.UpdatePriorityAsync(priorityId, updatePriorityDto, token);
                return Ok(new { message = "Update Successfully" });
            }
            catch (Exception ex)
            {
                // Tầng 2: lỗi nghiệp vụ (không tìm thấy, validate...)
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
