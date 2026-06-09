using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildLinkApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BuildLinkApi.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            Console.WriteLine("===== DbInitializer STARTED =====");

            await context.Database.MigrateAsync();

            Console.WriteLine($"Database: {context.Database.GetDbConnection().Database}");
            Console.WriteLine($"Server: {context.Database.GetDbConnection().DataSource}");

            await SeedRolesAsync(context);

            Console.WriteLine("===== DbInitializer FINISHED =====");
        }

        private static async Task SeedRolesAsync(AppDbContext context)
        {
            var beforeCount = await context.Roles.CountAsync();
            Console.WriteLine($"Roles count before seed: {beforeCount}");

            var defaultRoles = new List<Role>
        {
            new Role
            {
                Id = Guid.NewGuid(),
                Name = "Admin",
                Description = "System administrator with full access to manage the platform"
            },
            new Role
            {
                Id = Guid.NewGuid(),
                Name = "Staff",
                Description = "Internal staff member responsible for managing system operations and content"
            },
            new Role
            {
                Id = Guid.NewGuid(),
                Name = "CompanyOwner",
                Description = "Construction company owner with permission to manage company profile, projects, and company members"
            },
            new Role
            {
                Id = Guid.NewGuid(),
                Name = "CompanyMember",
                Description = "Member of a construction company with limited access to company-related features"
            },
            new Role
            {
                Id = Guid.NewGuid(),
                Name = "Customer",
                Description = "Customer account used to submit consultation requests and interact with the platform"
            }
        };

            foreach (var role in defaultRoles)
            {
                var existingRole = await context.Roles
                    .FirstOrDefaultAsync(x => x.Name == role.Name);

                if (existingRole == null)
                {
                    Console.WriteLine($"Adding role: {role.Name}");
                    await context.Roles.AddAsync(role);
                }
                else
                {
                    Console.WriteLine($"Updating role: {role.Name}");
                    existingRole.Description = role.Description;
                    existingRole.UpdatedAt = DateTime.UtcNow;
                }
            }

            var saved = await context.SaveChangesAsync();

            var afterCount = await context.Roles.CountAsync();
            Console.WriteLine($"SaveChanges result: {saved}");
            Console.WriteLine($"Roles count after seed: {afterCount}");
        }
    }
}