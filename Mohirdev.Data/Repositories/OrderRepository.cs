using Mohirdev.Data.Contexts;
using Mohirdev.Data.IRepositories;
using Mohirdev.Domain.Entities;

namespace Mohirdev.Data.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(MohirdevDbContext dbContext) : base(dbContext)
        {
        }
    }
}
