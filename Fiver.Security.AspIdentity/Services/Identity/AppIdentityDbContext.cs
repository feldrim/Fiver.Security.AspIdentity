using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Fiver.Security.AspIdentity.Services.Identity;

namespace Fiver.Security.AspIdentity.Services.Identity
{
    public class AppIdentityDbContext : IdentityDbContext<AppIdentityUser, AppIdentityRole, string>
    {
        public AppIdentityDbContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<Fiver.Security.AspIdentity.Services.Identity.AppIdentityUser> AppIdentityUser { get; set; }
    }
}