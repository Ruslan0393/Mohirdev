using Mohirdev.Data.Contexts;
using Mohirdev.Data.IRepositories;
using Mohirdev.Domain.Entities;

namespace Mohirdev.Data.Repositories
{
    public class ContentRepository : GenericRepository<Content>, IContentRepository
    {
        public ContentRepository(MohirdevDbContext dbContext) : base(dbContext)
        {
        }
    }
}
