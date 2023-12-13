using Microsoft.EntityFrameworkCore;
using Online_Store.Models;

namespace Online_Store.Data
{
    public class OnlineStoreDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            // Step3: Get the Section to Read from the Configuration File
            var configSection = configBuilder.GetSection("ConnectionStrings");

            // Step4: Get the Configuration Values based on the Config key
            var connectionString = configSection["SQLServerConnection"] ?? null;

            //Configuring the Connection String
            optionsBuilder.UseSqlServer(connectionString);
        }
        public DbSet<Products> Products { get; set; }

        public DbSet<Order> Orders { get; set; }
    }
}
