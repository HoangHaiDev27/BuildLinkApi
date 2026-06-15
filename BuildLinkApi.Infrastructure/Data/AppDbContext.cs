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
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

        public DbSet<Company> Companies => Set<Company>();
        public DbSet<CompanyCertificate> CompanyCertificates => Set<CompanyCertificate>();

        public DbSet<FileAsset> FileAssets => Set<FileAsset>();

        public DbSet<ProjectCategory> ProjectCategories => Set<ProjectCategory>();
        public DbSet<Project> Projects => Set<Project>();
        public DbSet<ProjectImage> ProjectImages => Set<ProjectImage>();

        public DbSet<ProductCategory> ProductCategories => Set<ProductCategory>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
        public DbSet<ServicePackage> ServicePackages => Set<ServicePackage>();
        public DbSet<Address> Addresses => Set<Address>();

        public DbSet<SystemSetting> SystemSettings => Set<SystemSetting>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Tự động apply toàn bộ Fluent API configuration trong assembly Infrastructure
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}