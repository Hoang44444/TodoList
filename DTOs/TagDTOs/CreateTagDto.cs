using System.ComponentModel.DataAnnotations;

namespace TodoList.DTOs.TagDTOs
{
    public class CreateTagDto
    {
        [Required(ErrorMessage = "Tag name is required.")]
        [MaxLength(100)]
        public string TagName { get; set; } = string.Empty;
    }
}
