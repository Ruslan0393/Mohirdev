using System.ComponentModel.DataAnnotations;

namespace Mohirdev.Service.DTOs.Category
{
    public class CreateCategoryDto
    {
        [Required]
        public string Name { get; set; }
    }
}
