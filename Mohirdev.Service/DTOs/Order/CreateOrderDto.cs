using System.ComponentModel.DataAnnotations;

namespace Mohirdev.Service.DTOs.Order
{
    public class CreateOrderDto
    {
        [Required]
        public long ClientId { get; set; }
        [Required]
        public long CourseId { get; set; }
    }
}
