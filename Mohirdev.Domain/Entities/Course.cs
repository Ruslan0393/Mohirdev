using Mohirdev.Domain.Commons;
using Mohirdev.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mohirdev.Domain.Entities
{
    public class Course : IAuditable
    {
        public long Id { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? ModifyAt { get; set; }
        public DateTime? DeleteAt { get; set; }
        public State State { get; set; }
        public string ImageName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        /// <summary>
        /// One to many relationship with Instructor and Course
        /// </summary>
        /// 
        public long UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public virtual ICollection<Category> Categories { get; }

        /// <summary>
        /// One to many reletionship with Course and Content
        /// </summary>
        public virtual ICollection<Content> Contents { get; set; }



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
