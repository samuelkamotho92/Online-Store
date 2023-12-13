using Microsoft.EntityFrameworkCore;
using Online_Store.Models;

namespace Online_Store.Data
{
    public class OnlineStoreDbContext:DbContext
    {



        public OnlineStoreDbContext(DbContextOptions<OnlineStoreDbContext> options) : base(options) { }
        public DbSet<Products> Products { get; set; }

        public DbSet<Order> Orders { get; set; }
    }
}
