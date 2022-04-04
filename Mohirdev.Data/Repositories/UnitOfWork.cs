using Mohirdev.Data.Contexts;
using Mohirdev.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mohirdev.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUserRepository User { get; private set; }
        public ICourseRepository Course { get; private set; }
        public IContentRepository Content { get; private set; }
        public IOrderRepository Order { get; private set; }
        public IStudentCourseRepository StudentCourses { get; private set; }

        private readonly MohirdevDbContext mohirdevDb;


        public UnitOfWork(MohirdevDbContext mohirdevDb)
        {
            this.mohirdevDb = mohirdevDb;
            User = new UserRepository(mohirdevDb);
            Course = new CourseRepository(mohirdevDb);
            Content = new ContentRepository(mohirdevDb);
            Order = new OrderRepository(mohirdevDb);
            StudentCourses = new StudentCoursesRepository(mohirdevDb);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task SaveChangesAsync()
        {
            await mohirdevDb.SaveChangesAsync();
        }
    }
}
