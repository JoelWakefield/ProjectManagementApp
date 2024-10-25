using Microsoft.EntityFrameworkCore;

namespace ReactApp.Server.Data
{
    public class ApplicationDbContext()
    {
        public DbSet<AppUser> Users { get; set; }
    }
}
