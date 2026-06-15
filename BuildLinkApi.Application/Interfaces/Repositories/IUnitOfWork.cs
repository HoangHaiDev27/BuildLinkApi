using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildLinkApi.Domain.Entities;

namespace BuildLinkApi.Application.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        IAccountRepository Accounts { get; }

        IGenericRepository<User> Users { get; }

        IRoleRepository Roles { get; }

        IRefreshTokenRepository RefreshTokens { get; }

        IGenericRepository<EmailVerificationToken> EmailVerificationTokens { get; }

        IGenericRepository<Company> Companies { get; }

        IGenericRepository<FileAsset> FileAssets { get; }

        IGenericRepository<ProjectCategory> ProjectCategories { get; }

        IGenericRepository<Project> Projects { get; }

        IGenericRepository<ProjectImage> ProjectImages { get; }

        IGenericRepository<SystemSetting> SystemSettings { get; }

        Task<int> SaveChangesAsync();
    }
}