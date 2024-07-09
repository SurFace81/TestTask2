using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Food> Foods { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserFood> UserFoods { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string dbPath = "app.db";
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }
    }
}
