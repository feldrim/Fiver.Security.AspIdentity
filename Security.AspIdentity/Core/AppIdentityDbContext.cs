using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Security.AspIdentity.Models;

namespace Security.AspIdentity.Core
{
    public class AppIdentityDbContext : IdentityDbContext<AppIdentityUser, AppIdentityRole, string>
    {
        public AppIdentityDbContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<AppIdentityUser> AppIdentityUser { get; set; }
    }
}