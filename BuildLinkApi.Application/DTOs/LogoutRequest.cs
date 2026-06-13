using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildLinkApi.Application.DTOs
{
    public class LogoutRequest
    {
        public string RefreshToken { get; set; } = string.Empty;
    }
}