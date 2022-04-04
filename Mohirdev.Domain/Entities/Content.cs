using Mohirdev.Domain.Commons;
using Mohirdev.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mohirdev.Domain.Entities
{
    public class Content : IAuditable
    {
        public long Id { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? ModifyAt { get; set; }
        public DateTime? DeleteAt { get; set; }
        public State State { get; set; }
        public string VideoUrl { get; set; }
        public string Name { get; set; }
        public string ExtraInfo { get; set; }
        public long CourseId { get; set; }
        [ForeignKey("CourseId")]
        public Course Course { get; set; }

        public void Update()
        {
            ModifyAt = DateTime.Now;
            State = State.Modified;
        }

        public void Create()
        {
            CreateAt = DateTime.Now;
            State = State.Created;
        }

        public void Delete()
        {
            State = State.Deleted;
        }

    }
}
