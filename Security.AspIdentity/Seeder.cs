using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Security.AspIdentity.Core;
using Security.AspIdentity.Models.Business;
using Security.AspIdentity.Models.Core;

namespace Security.AspIdentity
{
    public static class Seeder
    {
        private static AppIdentityDbContext _context;
        private static UserManager<CrmUser> _userManager;
        private static RoleManager<CrmRole> _roleManager;

        public static ArrayList Errors { get; set; }

        public static void Initialize(IServiceProvider serviceProvider)
        {
            _userManager = serviceProvider.GetRequiredService<UserManager<CrmUser>>();
            _roleManager = serviceProvider.GetRequiredService<RoleManager<CrmRole>>();
            _context = serviceProvider.GetRequiredService<AppIdentityDbContext>();
            _context.Database.EnsureCreated();
            Errors = new ArrayList();

            // Add identity data
            AddRoles();
            AddUsers();

            // Add business data
            AddPersonnel();
            AddCompany();
            AddDepartments();
            AddTitles();

            DumpErrors();
        }

        private static void AddRoles()
        {
            if (_context.Roles.Any()) return;

            var roles = new List<CrmRole>
            {
                new CrmRole
                {
                    Name = "DefaultUser",
                    Description = "Every user is a default user."
                },
                new CrmRole
                {
                    Name = "Admin",
                    Description = "At least one admin is needed."
                },
                new CrmRole
                {
                    Name = "PermissionManager",
                    Description = "A user role for managing user/role permissions."
                },
                new CrmRole
                {
                    Name = "PersonnelManager",
                    Description = "A user role for adding, modifying and deleting personnel."
                }
            };

            foreach (var role in roles)
            {
                var result = _roleManager.CreateAsync(role).Result;
                if (!result.Succeeded) AddErrors(result);
            }
        }

        private static void AddUsers()
        {
            if (_context.AppIdentityUser.Any()) return;

            // Create with demo users
            var users = new List<CrmUser>
            {
                new CrmUser
                {
                    UserName = "demo",
                    Email = "demo@shitty.com",
                    EmailConfirmed = true,
                    LockoutEnabled = false
                },
                new CrmUser
                {
                    UserName = "admin",
                    Email = "admin@shitty.com",
                    EmailConfirmed = true,
                    LockoutEnabled = false
                },
                new CrmUser
                {
                    UserName = "user",
                    Email = "user@shitty.com",
                    EmailConfirmed = true,
                    LockoutEnabled = false
                }
            };

            foreach (var user in users)
            {
                var result = _userManager.CreateAsync(user, "Password123!").Result;
                if (!result.Succeeded) AddErrors(result);
            }

            // Add fake users
            // FIX: I cannot add users!
            var fakeUsers = new Faker<CrmUser>()
                .RuleFor(u => u.UserName, f => f.Person.UserName)
                .RuleFor(u => u.Email, f => f.Person.Email)
                .RuleFor(u => u.EmailConfirmed, true)
                .GenerateLazy(30);

            foreach (var user in fakeUsers)
            {
                var result = _userManager.CreateAsync(user, $"{user.UserName}+000").Result;
                if (!result.Succeeded) AddErrors(result);
            }
        }

        private static void AddPersonnel()
        {
            //MAGIC NUMBER: 3 is the number of hand-coded users and connected personnel.
            var users = _context.AppIdentityUser.ToList();
            if (users.Count > 3) return;

            // Start with demo personnel 
            var list = new List<CrmPersonnel>
            {
                new CrmPersonnel
                {
                    UserData = users.FirstOrDefault(u => u.UserName.Equals("demo")),
                    FirstName = "demo",
                    LastName = "user"
                },
                new CrmPersonnel
                {
                    UserData = users.FirstOrDefault(u => u.UserName.Equals("admin")),
                    FirstName = "admin",
                    LastName = "user"
                },
                new CrmPersonnel
                {
                    UserData = users.FirstOrDefault(u => u.UserName.Equals("user")),
                    FirstName = "user",
                    LastName = "user"
                }
            };

            _context.CrmPersonnel.AddRange(list);
            _context.SaveChanges();

            if (users.Count == 3) return;

            // Create fake personnel
            var fakePersonnel = new Faker<CrmPersonnel>()
                .RuleFor(u => u.FirstName, f => f.Person.FirstName)
                .RuleFor(u => u.LastName, f => f.Person.LastName)
                .Generate(30);

            for (var i = list.Count; i < fakePersonnel.Count; i++) fakePersonnel[i].UserData = users[i];

            _context.CrmPersonnel.AddRange(fakePersonnel);
            _context.SaveChanges();
        }

        private static void AddCompany()
        {
            if (_context.CrmUnits.Any(u => u.Type.Equals("Company"))) return;

            // Start with demo company
            var list = new List<CrmUnit>
            {
                new CrmUnit
                {
                    Type = "Company",
                    Name = "Shitty Agency",
                    Description = "Some shitty motivational motto"
                }
            };

            // Add fake companies
            list.AddRange(new Faker<CrmUnit>().RuleFor(c => c.Name, f => f.Company.CompanyName())
                .RuleFor(c => c.Type, f => f.Company.CatchPhrase())
                .RuleFor(c => c.Type, "Company").Generate(5));

            _context.CrmUnits.AddRange(list);
            _context.SaveChanges();
        }

        private static void AddDepartments()
        {
            var units = _context.CrmUnits.ToList();
            if (units.Any(u => u.Type.Equals("Department"))) return;

            var companies = units.Where(u => u.Type.Equals("Company")).ToList();
            var fakeCompanies = companies.Where(c => !c.Name.Equals("Shitty Company")).ToList();

            // Start with demo departments
            var list = new List<CrmUnit>
            {
                new CrmUnit
                {
                    Type = "Department",
                    Name = "Marketing",
                    Description = "Licks the boss' ass",
                    Parent = companies.FirstOrDefault(p => p.Name == "Shitty Agency")
                },
                new CrmUnit
                {
                    Type = "Department",
                    Name = "Sales",
                    Description = "Licks the customer's ass",
                    Parent = companies.FirstOrDefault(p => p.Name == "Shitty Agency")
                },
                new CrmUnit
                {
                    Type = "Department",
                    Name = "Manufacturing",
                    Description = "Sweats te shit outta their ass",
                    Parent = companies.FirstOrDefault(p => p.Name == "Shitty Agency")
                }
            };

            // Add fake departments
            var depts = new[]
                {"Marketing", "Sales", "Human Resources", "IT", "Design", "R&D", "Manufacturing", "Logistics"};

            var fakeDepts =
                new Faker<CrmUnit>()
                    .RuleFor(c => c.Name, f => f.PickRandom(depts))
                    .RuleFor(c => c.Type, f => f.Company.CatchPhrase())
                    .RuleFor(c => c.Type, "Department")
                    .Generate(30);
            foreach (var dept in fakeDepts)
            {
                var random = new Random().Next(0, fakeDepts.Count);
                dept.Parent = fakeCompanies[random];
                dept.ParentId = dept.Parent.ParentId;
            }

            _context.CrmUnits.AddRange(list);
            _context.SaveChanges();
        }

        private static void AddTitles()
        {
            if (_context.CrmTitles.Any()) return;

            var list = new List<CrmTitle>
            {
                new CrmTitle
                {
                    Title = "Demo Title",
                    Subtitle = "...",
                    Description = "..."
                },
                new CrmTitle
                {
                    Title = "Admin Title",
                    Subtitle = "...",
                    Description = "..."
                },
                new CrmTitle
                {
                    Title = "User Title",
                    Subtitle = "...",
                    Description = "..."
                }
            };

            var titles = new[]
            {
                "Account Collector", "Accounting Clerk", "Administrative Assistant", "Administrative Coordinator",
                "Administrative Director", "Administrative Manager", "Administrative Services Manager",
                "Administrative Services Officer", "Administrative Specialist", "Administrative Support Manager",
                "Administrative Support Supervisor", "Administrator", "Assistant Director", "Auditing Clerk",
                "Bill Collector", "Billing Clerk", "Billing Coordinator", "Bookkeeper", "Client Relations Manager",
                "Contract Administrator", "Credit Clerk", "Data Entry", "Executive Assistant",
                "Executive Services Administrator", "Facility Manager", "File Clerk", "Financial Clerk",
                "General Office Clerk", "Human Resources Administrator", "Information Clerk", "Legal Secretary",
                "Mail Clerk", "Mail Clerk Leader", "Material Recording Clerk", "Medical Secretary", "Office Assistant",
                "Office Clerk", "Office Manager", "Office Support Manager", "Office Support Supervisor",
                "Program Administrator", "Program Manager", "Receptionist", "Records Management Analyst", "Secretary",
                "Senior Administrative Analyst", "Senior Administrative Coordinator",
                "Senior Administrative Services Officer", "Senior Coordinator", "Senior Executive Assistant",
                "Senior Special Events Coordinator", "Senior Support Assistant", "Senior Support Specialist",
                "Special Events Coordinator", "Special Programs Coordinator", "Staff Assistant", "Support Assistant",
                "Support Specialist", "Typist", "Virtual Assistant", "Virtual Receptionist", "Word Processor"
            };

            var fakeTitles = new Faker<CrmTitle>()
                .RuleFor(u => u.Title, f => f.PickRandom(titles))
                .RuleFor(u => u.Subtitle, "...")
                .RuleFor(u => u.Description, "...")
                .Generate(60);

            list.AddRange(fakeTitles);
            _context.CrmTitles.AddRange(list);
            _context.SaveChanges();
        }

        private static void AddErrors<T>(T error) where T : class
        {
            Errors.Add(error);
        }

        private static void DumpErrors()
        {
            foreach (var error in Errors) Console.WriteLine(error);
        }
    }
}