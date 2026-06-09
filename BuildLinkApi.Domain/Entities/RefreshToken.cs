using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildLinkApi.Domain.Common;

namespace BuildLinkApi.Domain.Entities
{
    public class RefreshToken
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid AccountId { get; set; }

        public string Token { get; set; } = string.Empty;

        public DateTime ExpiredAt { get; set; }

        public DateTime? RevokedAt { get; set; }

        public bool IsRevoked { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Account Account { get; set; } = null!;
    }
}