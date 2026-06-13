using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildLinkApi.Application.DTOs
{
    public class AuthResponse
    {
        public Guid AccountId { get; set; }

        public Guid? UserId { get; set; }

        public Guid? CompanyId { get; set; }

        public string Email { get; set; } = string.Empty;

        public string AccountType { get; set; } = string.Empty;

        public List<string> Roles { get; set; } = new();

        public string AccessToken { get; set; } = string.Empty;

        public string RefreshToken { get; set; } = string.Empty;

        public DateTime AccessTokenExpiresAt { get; set; }

        public DateTime RefreshTokenExpiresAt { get; set; }
    }
}