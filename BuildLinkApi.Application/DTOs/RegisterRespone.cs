using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildLinkApi.Application.DTOs
{
    public class RegisterRespone
    {
        public Guid AccountId { get; set; }

        public string Email { get; set; } = string.Empty;

        public string RoleName { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}