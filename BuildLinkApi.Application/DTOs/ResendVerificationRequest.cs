using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildLinkApi.Application.DTOs
{
    public class ResendVerificationRequest
    {
        public string Email { get; set; } = string.Empty;
    }
}