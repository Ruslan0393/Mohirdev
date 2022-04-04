using Microsoft.EntityFrameworkCore;
using Mohirdev.Domain.Entities;


namespace Mohirdev.Data.Contexts
{
    public class MohirdevDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Content> Contents { get; set; }

        public DbSet<Order> Orders { get; set; }

        public MohirdevDbContext(DbContextOptions<MohirdevDbContext> options) : base(options)
        { }

    }
}
