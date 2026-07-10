using System.ComponentModel.DataAnnotations;

namespace TodoList.DTOs.PriorityDTOs
{
    public class CreatePriorityDto
    {
        [Required(ErrorMessage = "PriorityName is required")]
        [MaxLength(50)]
        public string PriorityName { get; set; } = string.Empty;
    }
}
