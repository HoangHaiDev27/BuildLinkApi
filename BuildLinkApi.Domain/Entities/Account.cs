using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildLinkApi.Domain.Common;
using BuildLinkApi.Domain.Enums;

namespace BuildLinkApi.Domain.Entities
{
    public class Account : BaseEntity
    {
        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public AccountType AccountType { get; set; }

        public AccountStatus Status { get; set; } = AccountStatus.Active;

        public Guid? UserId { get; set; }

        public Guid? CompanyId { get; set; }

        public DateTime? LastLoginAt { get; set; }

        public DateTime? EmailVerifiedAt { get; set; }

        public User? User { get; set; }

        public Company? Company { get; set; }


        public ICollection<AccountRole> AccountRoles { get; set; } = new List<AccountRole>();

        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}