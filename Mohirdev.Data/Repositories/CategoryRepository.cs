using Mohirdev.Data.Contexts;
using Mohirdev.Data.IRepositories;
using Mohirdev.Domain.Entities;

namespace Mohirdev.Data.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(MohirdevDbContext dbContext) : base(dbContext)
        {
        }
    }
}
