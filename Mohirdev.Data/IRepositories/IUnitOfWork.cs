using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mohirdev.Data.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        public IUserRepository User { get; }
        public ICourseRepository Course { get; }
        public IContentRepository Content { get; }
        public IOrderRepository Order { get; }
        public IStudentCourseRepository StudentCourses { get; }

        Task SaveChangesAsync();
    }
}
