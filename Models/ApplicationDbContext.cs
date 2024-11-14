using Microsoft.EntityFrameworkCore;

namespace atc_backend.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Define your DbSets here
        public DbSet<User> Users { get; set; }
    }
}
