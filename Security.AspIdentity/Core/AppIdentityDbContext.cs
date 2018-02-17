using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Security.AspIdentity.Models.Business;
using Security.AspIdentity.Models.Core;

namespace Security.AspIdentity.Core
{
    public class AppIdentityDbContext : IdentityDbContext<CrmUser, CrmRole, string>
    {
        public AppIdentityDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<CrmUser> AppIdentityUser { get; set; }
        public DbSet<CrmPersonnel> CrmPersonnel { get; set; }
        public DbSet<CrmTitle> CrmTitles { get; set; }
        public DbSet<CrmTitlePersonnel> CrmTitlePersonnels { get; set; }
        public DbSet<CrmTitleRole> CrmTitleRoles { get; set; }
        public DbSet<CrmUnit> CrmUnits { get; set; }
        public DbSet<CrmUnitTitle> CrmUnitTitles { get; set; }
    }
}