using System.Linq;
using WebApplication1.Models;

namespace WebApplication1.Models
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
