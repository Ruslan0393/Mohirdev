using Mohirdev.Domain.Commons;
using Mohirdev.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mohirdev.Domain.Entities
{
    public class Order : IAuditable
    {
        public long Id { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime? ModifyAt { get; set; }
        public DateTime? DeleteAt { get; set; }
        public State State { get; set; }
        public long ClientId { get; set; }
        public long CourseId { get; set; }
        public long Amount { get; set; }

    }
}
