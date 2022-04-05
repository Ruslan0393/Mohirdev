using System.ComponentModel.DataAnnotations.Schema;

namespace Mohirdev.Domain.Entities
{
    public class StudentCourses
    {
        public long Id { get; set; }
        public long CourseId { get; set; }

        public long UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
