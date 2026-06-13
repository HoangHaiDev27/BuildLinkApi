using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildLinkApi.Domain.Entities;

namespace BuildLinkApi.Application.DTOs
{
    public class RegisterRequest
    {
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string ConfirmPassword { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string RoleName { get; set; } = string.Empty;

        public string? UserName { get; set; } = string.Empty;

        public string? CompanyName { get; set; } = string.Empty;


    }
}