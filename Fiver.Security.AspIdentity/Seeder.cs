using System;
using System.Linq;
using Fiver.Security.AspIdentity.Services.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Fiver.Security.AspIdentity
{
    public static class Seeder
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<AppIdentityDbContext>();
            context.Database.EnsureCreated();
            if (!context.Roles.Any())
            {
                context.Roles.Add(new AppIdentityRole
                {
                    Name = "DefaultUser",
                    Description = "Every user is a default user.",
                    NormalizedName = "DefaultUser".Normalize()
                });
                context.Roles.Add(new AppIdentityRole
                {
                    Name = "Admin",
                    Description = "At least one admin is needed.",
                    NormalizedName = "Admin".Normalize()
                });
            }

            var users = context.Users;
            var roleId = context.Roles.FirstOrDefault()?.Id;
            foreach (var user in users)
            {
                var data = context.UserRoles.Find(user.Id, roleId);
                if (data == null)
                    context.UserRoles.Add(new IdentityUserRole<string> { RoleId = roleId, UserId = user.Id });
            }
            context.SaveChanges();
        }
    }
}
