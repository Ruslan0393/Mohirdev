using Mohirdev.Data.Contexts;
using Mohirdev.Data.IRepositories;
using Mohirdev.Domain.Entities;


namespace Mohirdev.Data.Repositories
{
    public class StudentCoursesRepository : GenericRepository<StudentCourses>, IStudentCourseRepository
    {
        public StudentCoursesRepository(MohirdevDbContext dbContext) : base(dbContext)
        {
        }
    }
}
