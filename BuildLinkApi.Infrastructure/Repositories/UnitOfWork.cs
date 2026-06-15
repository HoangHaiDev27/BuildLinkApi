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

            Accounts = new AccountRepository(_context);
            Users = new GenericRepository<User>(_context);
            Roles = new RoleRepository(_context);
            RefreshTokens = new RefreshTokenRepository(_context);

            Companies = new GenericRepository<Company>(_context);
            FileAssets = new GenericRepository<FileAsset>(_context);

            ProjectCategories = new GenericRepository<ProjectCategory>(_context);
            Projects = new GenericRepository<Project>(_context);
            ProjectImages = new GenericRepository<ProjectImage>(_context);

            SystemSettings = new GenericRepository<SystemSetting>(_context);

            EmailVerificationTokens = new GenericRepository<EmailVerificationToken>(_context);
        }

        public IAccountRepository Accounts { get; }

        public IGenericRepository<User> Users { get; }

        public IRoleRepository Roles { get; }

        public IRefreshTokenRepository RefreshTokens { get; }

        public IGenericRepository<Company> Companies { get; }

        public IGenericRepository<FileAsset> FileAssets { get; }

        public IGenericRepository<ProjectCategory> ProjectCategories { get; }

        public IGenericRepository<Project> Projects { get; }

        public IGenericRepository<ProjectImage> ProjectImages { get; }

        public IGenericRepository<SystemSetting> SystemSettings { get; }

        public IGenericRepository<EmailVerificationToken> EmailVerificationTokens { get; }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}