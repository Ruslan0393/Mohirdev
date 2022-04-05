using Mohirdev.Data.Contexts;
using Mohirdev.Data.IRepositories;
using Mohirdev.Domain.Entities;

namespace Mohirdev.Data.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(MohirdevDbContext dbContext) : base(dbContext)
        {
        }
    }
}
