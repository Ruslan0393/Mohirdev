using Microsoft.AspNetCore.Http;
using Mohirdev.Service.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Mohirdev.Service.DTOs.Content
{
    public class CreateContentDto
    {
        [Required]
        [MaxFileSize(153600)]
        public IFormFile Video { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ExtraInfo { get; set; }
        [Required]
        public long CourseId { get; set; }
    }
}
