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
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        private readonly AppDbContext _context;
        public RoleRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Role?> GetByNameAsync(string name)
        {
            return await _context.Roles.FirstOrDefaultAsync(x => x.Name == name && !x.IsDeleted);
        }
    }
}