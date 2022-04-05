using Mohirdev.Domain.Enums;
using System;

namespace Mohirdev.Domain.Commons
{
    public interface IAuditable
    {
        public long Id { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? ModifyAt { get; set; }
        public DateTime? DeleteAt { get; set; }
        public State State { get; set; }
    }
}
