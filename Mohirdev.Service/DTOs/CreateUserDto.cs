using Microsoft.AspNetCore.Http;
using Mohirdev.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mohirdev.Service.DTOs
{
    public class CreateUserDto
    {
        public Role Role { get; set; }
        public IFormFile ImageName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public decimal Balance { get; set; }
    }
}
