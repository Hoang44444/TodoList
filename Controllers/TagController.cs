using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.DTOs.TagDTOs;
using TodoList.Service;

namespace TodoList.Controllers
{
    [ApiController]
    [Route("api/[controller]")]     // -> api/tag
    public class TagController : Controller
    {
        private readonly ITagService _tagService;
        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTag([FromBody] CreateTagDto createTagDto, CancellationToken token)
        {
            try
            {
                await _tagService.CreateTagAsync(createTagDto, token);
                return Ok(new { message = "Create Successfully" });

            }
            catch (DbUpdateException ex)
            {
                // Tầng 3: lỗi khi lưu (FK sai, trùng key, DB lỗi...)
                return Problem(
                    title: "Can not save the entity",
                    detail: ex.InnerException?.Message ?? ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{tagId:int}")]
        public async Task<IActionResult> UpdateTag(int tagId, [FromBody] UpdateTagDto updateTagDto, CancellationToken token)
        {
            try
            {
                await _tagService.UpdateTagAsync(tagId, updateTagDto, token);
                return Ok(new { message = "Update Successfully" });
            }
            catch (DbUpdateException ex)
            {
                // Tầng 3: lỗi khi lưu (FK sai, trùng key, DB lỗi...)
                return Problem(
                    title: "Can not save the entity",
                    detail: ex.InnerException?.Message ?? ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                // Tầng 2: lỗi nghiệp vụ (không tìm thấy, validate...)
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
