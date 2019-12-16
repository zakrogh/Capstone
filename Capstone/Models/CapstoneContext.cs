using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Capstone.Models
{
    public class CapstoneContext : IdentityDbContext<ApplicationUser>
  {
    public DbSet<User> Users { get; set; }

    public CapstoneContext(DbContextOptions options) : base(options) { }
  }
}
