using Mohirdev.Domain.Commons;
using Mohirdev.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Mohirdev.Domain.Entities
{
    public class Category : IAuditable
    {
        public long Id { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? ModifyAt { get; set; }
        public DateTime? DeleteAt { get; set; }
        public State State { get; set; }
        public string Name { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}
