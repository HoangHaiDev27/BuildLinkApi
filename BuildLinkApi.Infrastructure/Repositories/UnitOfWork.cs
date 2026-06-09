using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildLinkApi.Application.Interfaces.Repositories;
using BuildLinkApi.Domain.Entities;
using BuildLinkApi.Infrastructure.Data;

namespace BuildLinkApi.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;

            Accounts = new GenericRepository<Account>(_context);
            Users = new GenericRepository<User>(_context);
            Roles = new GenericRepository<Role>(_context);
            AccountRoles = new GenericRepository<AccountRole>(_context);
            RefreshTokens = new GenericRepository<RefreshToken>(_context);

            Companies = new GenericRepository<Company>(_context);
            FileAssets = new GenericRepository<FileAsset>(_context);

            ProjectCategories = new GenericRepository<ProjectCategory>(_context);
            Projects = new GenericRepository<Project>(_context);
            ProjectImages = new GenericRepository<ProjectImage>(_context);

            SystemSettings = new GenericRepository<SystemSetting>(_context);
        }

        public IGenericRepository<Account> Accounts { get; }

        public IGenericRepository<User> Users { get; }

        public IGenericRepository<Role> Roles { get; }

        public IGenericRepository<AccountRole> AccountRoles { get; }

        public IGenericRepository<RefreshToken> RefreshTokens { get; }

        public IGenericRepository<Company> Companies { get; }

        public IGenericRepository<FileAsset> FileAssets { get; }

        public IGenericRepository<ProjectCategory> ProjectCategories { get; }

        public IGenericRepository<Project> Projects { get; }

        public IGenericRepository<ProjectImage> ProjectImages { get; }

        public IGenericRepository<SystemSetting> SystemSettings { get; }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}