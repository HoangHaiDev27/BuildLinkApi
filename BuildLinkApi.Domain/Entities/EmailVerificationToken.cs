using System;
using BuildLinkApi.Domain.Common;

namespace BuildLinkApi.Domain.Entities
{
    public class EmailVerificationToken : BaseEntity
    {
        public Guid AccountId { get; set; }

        public string Code { get; set; } = string.Empty;

        public DateTime ExpiresAt { get; set; }

        public bool IsUsed { get; set; }

        public Account Account { get; set; } = null!;
    }
}
