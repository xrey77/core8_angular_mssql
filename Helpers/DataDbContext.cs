using Microsoft.EntityFrameworkCore;
using core8_angular_mssql.Entities;
using Microsoft.Extensions.Configuration;

namespace core8_angular_mssql.Helpers
{

   public class DataDbContext : DbContext
    {

        protected readonly IConfiguration Configuration;

        public DataDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        }


        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
    }

}