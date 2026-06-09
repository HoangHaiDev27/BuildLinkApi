using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildLinkApi.Domain.Entities;

namespace BuildLinkApi.Application.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        IGenericRepository<Account> Accounts { get; }

        IGenericRepository<User> Users { get; }

        IGenericRepository<Role> Roles { get; }

        IGenericRepository<AccountRole> AccountRoles { get; }

        IGenericRepository<RefreshToken> RefreshTokens { get; }

        IGenericRepository<Company> Companies { get; }

        IGenericRepository<FileAsset> FileAssets { get; }

        IGenericRepository<ProjectCategory> ProjectCategories { get; }

        IGenericRepository<Project> Projects { get; }

        IGenericRepository<ProjectImage> ProjectImages { get; }

        IGenericRepository<SystemSetting> SystemSettings { get; }

        Task<int> SaveChangesAsync();
    }
}