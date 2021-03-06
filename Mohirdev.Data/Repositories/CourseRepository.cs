using Mohirdev.Data.Contexts;
using Mohirdev.Data.IRepositories;
using Mohirdev.Domain.Entities;

namespace Mohirdev.Data.Repositories
{
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        public CourseRepository(MohirdevDbContext dbContext) : base(dbContext)
        {
        }
    }
}
