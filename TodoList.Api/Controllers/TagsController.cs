using Microsoft.AspNetCore.Mvc;
using TodoList.DTOs.TagDTOs;
using TodoList.Services;

namespace TodoList.Controllers
{
    [ApiController]
    [Route("api/[controller]")]     // -> api/tags
    public class TagsController : ControllerBase
    {
        private readonly ITagService _tagService;
        public TagsController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTag([FromBody] CreateTagDto createTagDto, CancellationToken token)
        {
            await _tagService.CreateTagAsync(createTagDto, token);
            return Ok(new { message = "Create Successfully" });
        }

        [HttpPut("{tagId:int}")]
        public async Task<IActionResult> UpdateTag(int tagId, [FromBody] UpdateTagDto updateTagDto, CancellationToken token)
        {
            await _tagService.UpdateTagAsync(tagId, updateTagDto, token);
            return Ok(new { message = "Update Successfully" });
        }

        [HttpDelete("{tagId:int}")]
        public async Task<IActionResult> DeleteTag(int tagId, CancellationToken token)
        {
            await _tagService.DeleteTagAsync(tagId, token);
            return Ok(new { message = "Delete Successfully" });
        }

        [HttpGet("{tagId:int}")]
        public async Task<IActionResult> GetTagById(int tagId, CancellationToken token)
        {
            var tagResponse = await _tagService.GetTagByIdAsync(tagId, token);
            return Ok(tagResponse);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTags(CancellationToken token)
        {
            var tags = await _tagService.GetAllTagsAsync(token);
            return Ok(tags);
        }
    }
}
