using Microsoft.AspNetCore.Http;
using Mohirdev.Domain.Enums;
using Mohirdev.Service.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Mohirdev.Service.DTOs
{
    public class CreateUserDto
    {
        [Required]
        public Role Role { get; set; }
        [Required]
        [MaxFileSize(2048)]
        public IFormFile ImageName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Email is not valid")]
        public string Email { get; set; }
        [Required]
        public decimal Balance { get; set; }
    }
}
