using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildLinkApi.Application.Common;
using BuildLinkApi.Application.DTOs;

namespace BuildLinkApi.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<ApiResponse<AuthResponse>> LoginAsync(LoginRequest request);

        Task<ApiResponse<CurrentAccountResponse>> GetCurrentAccountAsync(Guid accountId);

        Task<ApiResponse<RegisterRespone>> RegisterAsync(RegisterRequest request);

        Task<ApiResponse<bool>> LogoutAsync(LogoutRequest request);

        Task<ApiResponse<AuthResponse>> RefreshTokenAsync(RefreshTokenRequest request);
    }
}