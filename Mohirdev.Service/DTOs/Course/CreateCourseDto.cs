using Microsoft.AspNetCore.Http;
using Mohirdev.Service.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Mohirdev.Service.DTOs
{
    public class CreateCourseDto
    {
        [Required]
        [MaxFileSize(2048)]
        public IFormFile Image { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public long UserId { get; set; }
        [Required]
        public long CategoryId { get; set; }
    }
}
