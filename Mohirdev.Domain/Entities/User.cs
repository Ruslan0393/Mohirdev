using Mohirdev.Domain.Commons;
using Mohirdev.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Mohirdev.Domain.Entities
{
    public class User : IAuditable
    {
        public long Id { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? ModifyAt { get; set; }
        public DateTime? DeleteAt { get; set; }
        public State State { get; set; }
        public Role Role { get; set; }
        public string ImageName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public decimal Balance { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<StudentCourses> StudentCourses { get; set; }
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
