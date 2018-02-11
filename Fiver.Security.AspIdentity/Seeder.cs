using System;
using System.Linq;
using Fiver.Security.AspIdentity.Services.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Fiver.Security.AspIdentity
{
    public static class Seeder
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<AppIdentityDbContext>();
            context.Database.EnsureCreated();
            if (context.Roles.Any()) return;
            context.Roles.Add(new AppIdentityRole { Name = "DefaultUser", Description = "Every user is a default user.", NormalizedName = "DefaultUser".Normalize() });
            context.Roles.Add(new AppIdentityRole { Name = "Admin", Description = "At least one admin is needed.", NormalizedName = "Admin".Normalize() });
            context.SaveChanges();
        }
    }
}
