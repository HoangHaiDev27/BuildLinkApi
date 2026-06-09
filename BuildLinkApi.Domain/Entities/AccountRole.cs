using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildLinkApi.Domain.Common;

namespace BuildLinkApi.Domain.Entities
{
    public class AccountRole
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid AccountId { get; set; }

        public Guid RoleId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Account Account { get; set; } = null!;

        public Role Role { get; set; } = null!;
    }
}