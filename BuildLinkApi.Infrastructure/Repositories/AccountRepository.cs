using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildLinkApi.Application.Interfaces.Repositories;
using BuildLinkApi.Domain.Entities;
using BuildLinkApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BuildLinkApi.Infrastructure.Repositories
{
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        private readonly AppDbContext _context;

        public AccountRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Account?> GetByEmailAsync(string email)
        {
            return await _context.Accounts.FirstOrDefaultAsync(x => x.Email == email && !x.IsDeleted);
        }

        public async Task<Account?> GetByEmailWithRolesAsync(string email)
        {
            return await _context.Accounts.Include(x => x.AccountRoles).ThenInclude(x => x.Role).FirstOrDefaultAsync(x => x.Email == email && !x.IsDeleted);
        }

        public async Task<Account?> GetByIdWithRolesAsync(Guid accountId)
        {
            return await _context.Accounts.Include(x => x.AccountRoles).ThenInclude(x => x.Role).FirstOrDefaultAsync(x => x.Id == accountId && x.IsDeleted);
        }
    }
}