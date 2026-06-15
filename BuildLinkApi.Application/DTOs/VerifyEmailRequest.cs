using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildLinkApi.Application.DTOs
{
    public class VerifyEmailRequest
    {
        public string Email { get; set; } = string.Empty;

        public string Code { get; set; } = string.Empty;
    }
}