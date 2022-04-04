using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mohirdev.Service.DTOs.Order
{
    public class CreateOrderDto
    {
        public long ClientId { get; set; }
        public long CourseId { get; set; }
    }
}
