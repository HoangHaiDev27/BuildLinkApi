using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildLinkApi.Application.DTOs
{
    public class CurrentAccountResponse
    {
        public Guid AccountId { get; set; }

        public Guid? UserId { get; set; }

        public Guid? CompanyId { get; set; }

        public string Email { get; set; } = string.Empty;

        public string AccountType { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;
    }
}