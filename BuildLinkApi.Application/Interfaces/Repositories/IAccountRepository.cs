using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildLinkApi.Domain.Entities;

namespace BuildLinkApi.Application.Interfaces.Repositories
{
    public interface IAccountRepository : IGenericRepository<Account>
    {
        Task<Account?> GetByEmailAsync(string email);

        Task<Account?> GetByEmailWithRoleAsync(string email);

        Task<Account?> GetByIdWithRoleAsync(Guid accountId);


    }
}