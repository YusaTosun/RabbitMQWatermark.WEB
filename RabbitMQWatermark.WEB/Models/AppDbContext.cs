using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace RabbitMQWatermark.WEB.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }
        public DbSet<Product> Products { get; set; }
    }
}
