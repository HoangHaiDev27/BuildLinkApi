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
    public class RefreshTokenRepository : GenericRepository<RefreshToken>, IRefreshTokenRepository
    {
        private readonly AppDbContext _context;
        public RefreshTokenRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<RefreshToken?> GetByTokenAsync(string token)
        {
            return await _context.RefreshTokens
                .Include(x => x.Account)
                    .ThenInclude(x => x.Role)
                .FirstOrDefaultAsync(x => x.Token == token);
        }
    }
}