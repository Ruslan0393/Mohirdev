using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mohirdev.Service.DTOs.Content
{
    public class CreateContentDto
    {
        public IFormFile Video { get; set; }
        public string Name { get; set; }
        public string ExtraInfo { get; set; }
        public long CourseId { get; set; }
    }
}
