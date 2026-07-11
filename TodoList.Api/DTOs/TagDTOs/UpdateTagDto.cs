using System.ComponentModel.DataAnnotations;

namespace TodoList.DTOs.TagDTOs
{
    public class UpdateTagDto
    {
        [MaxLength(100)]
        public string TagName { get; set; } = string.Empty;
    }
}
