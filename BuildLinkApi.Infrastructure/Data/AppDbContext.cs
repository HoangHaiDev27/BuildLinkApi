using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildLinkApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BuildLinkApi.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }
        public DbSet<Account> Accounts => Set<Account>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<AccountRole> AccountRoles => Set<AccountRole>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

        public DbSet<Company> Companies => Set<Company>();
        public DbSet<CompanyCertificate> CompanyCertificates => Set<CompanyCertificate>();

        public DbSet<FileAsset> FileAssets => Set<FileAsset>();

        public DbSet<ProjectCategory> ProjectCategories => Set<ProjectCategory>();
        public DbSet<Project> Projects => Set<Project>();
        public DbSet<ProjectImage> ProjectImages => Set<ProjectImage>();

        public DbSet<SystemSetting> SystemSettings => Set<SystemSetting>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Tự động apply toàn bộ Fluent API configuration trong assembly Infrastructure
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}