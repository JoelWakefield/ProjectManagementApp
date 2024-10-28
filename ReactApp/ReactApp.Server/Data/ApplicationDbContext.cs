using Microsoft.EntityFrameworkCore;

namespace ReactApp.Server.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<AppUser> Users { get; set; }
    }
}
