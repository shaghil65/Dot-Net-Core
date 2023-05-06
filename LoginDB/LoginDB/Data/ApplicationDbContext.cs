using LoginDB.Models;
using Microsoft.EntityFrameworkCore;

namespace LoginDB.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Login> UserDetails { get; set; }
    }
}
