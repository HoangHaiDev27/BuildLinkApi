using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BuildLinkApi.Application.Common;
using BuildLinkApi.Application.DTOs;
using BuildLinkApi.Application.Interfaces.Repositories;
using BuildLinkApi.Application.Interfaces.Services;
using BuildLinkApi.Domain.Entities;
using BuildLinkApi.Domain.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BuildLinkApi.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly JwtSettings _jwtSettings;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IPasswordHasher _hasher;

        private readonly IMapper _mapper;

        public AuthService(IConfiguration configuration, IUnitOfWork unitOfWork, IMapper mapper, IPasswordHasher hasher)
        {
            _jwtSettings = configuration
        .GetSection("Jwt")
        .Get<JwtSettings>() ?? new JwtSettings();
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hasher = hasher;
        }
        public async Task<ApiResponse<CurrentAccountResponse>> GetCurrentAccountAsync(Guid accountId)
        {
            var account = await _unitOfWork.Accounts.GetByIdAsync(accountId);

            if (account == null)
            {
                return ApiResponse<CurrentAccountResponse>.Fail("Account not found");
            }
            var response = _mapper.Map<CurrentAccountResponse>(account);

            return ApiResponse<CurrentAccountResponse>.Ok(response, "Current account loaded successfully");
        }

        public async Task<ApiResponse<AuthResponse>> LoginAsync(LoginRequest request)
        {
            var account = await _unitOfWork.Accounts.GetByEmailAsync(request.Email);

            if (account == null)
            {
                return ApiResponse<AuthResponse>.Fail("Email is not foud");
            }
            if (account.Status != AccountStatus.Active)
            {
                return ApiResponse<AuthResponse>.Fail("Emaild is not valid");
            }
            if (!_hasher.VerifyPassword(request.Password, account.PasswordHash))
            {
                return ApiResponse<AuthResponse>.Fail("Password is not valid");
            }

            account.LastLoginAt = DateTime.Now;

            var roles = account.AccountRoles.Select(x => x.Role.Name).ToList();

            var authResponse = await BuildAuthResponseAsync(account, roles);

            await _unitOfWork.SaveChangesAsync();

            return ApiResponse<AuthResponse>.Ok(authResponse, "Login successful");
        }
        public async Task<ApiResponse<RegisterRespone>> RegisterAsync(RegisterRequest request)
        {
            if (request == null)
            {
                return ApiResponse<RegisterRespone>.Fail("Register request is required");
            }

            var email = request.Email.Trim().ToLower();

            var existingAccount = await _unitOfWork.Accounts.GetByEmailAsync(email);

            if (existingAccount != null)
            {
                return ApiResponse<RegisterRespone>.Fail("Email already exists");
            }

            if (request.Password != request.ConfirmPassword)
            {
                return ApiResponse<RegisterRespone>.Fail("Password does not match");
            }

            var role = await _unitOfWork.Roles.GetByNameAsync(request.RoleName);

            if (role == null)
            {
                return ApiResponse<RegisterRespone>.Fail("Role does not exist");
            }

            var account = new Account
            {
                Id = Guid.NewGuid(),
                Email = email,
                PasswordHash = _hasher.HashPassword(request.Password),
                Status = AccountStatus.Active,
                CreatedAt = DateTime.UtcNow
            };

            if (role.Name == "Customer")
            {
                var user = new User
                {
                    Id = Guid.NewGuid(),
                    FullName = request.UserName!.Trim(),
                    PhoneNumber = request.PhoneNumber,
                    CreatedAt = DateTime.UtcNow
                };

                account.UserId = user.Id;
                account.AccountType = AccountType.Customer;

                await _unitOfWork.Users.AddAsync(user);
            }
            else
            {
                var company = new Company
                {
                    Id = Guid.NewGuid(),
                    CompanyName = request.CompanyName!.Trim(),
                    Slug = request.CompanyName.Trim().ToLower().Replace(" ", "-"),
                    CreatedAt = DateTime.UtcNow
                };

                account.CompanyId = company.Id;
                account.AccountType = AccountType.Company;

                await _unitOfWork.Companies.AddAsync(company);
            }

            var accountRole = new AccountRole
            {
                Id = Guid.NewGuid(),
                AccountId = account.Id,
                RoleId = role.Id,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Accounts.AddAsync(account);
            await _unitOfWork.AccountRoles.AddAsync(accountRole);

            await _unitOfWork.SaveChangesAsync();

            var result = new RegisterRespone
            {
                AccountId = account.Id,
                Email = account.Email,
                RoleName = role.Name,
                CreatedAt = account.CreatedAt
            };

            return ApiResponse<RegisterRespone>.Ok(result, "Register successful");
        }
        private async Task<AuthResponse> BuildAuthResponseAsync(Account account, List<string> roles)
        {
            var accessTokenExpiresAt = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpireMinutes);
            var refreshTokenExpiresAt = DateTime.UtcNow.AddDays(7);
            var accessToken = GenerateAccessToken(account, roles, accessTokenExpiresAt);
            var refreshTokenValue = GenerateRefreshToken();
            var refreshToken = new RefreshToken
            {
                Id = Guid.NewGuid(),
                AccountId = account.Id,
                Token = refreshTokenValue,
                ExpiredAt = refreshTokenExpiresAt,
                IsRevoked = false,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.RefreshTokens.AddAsync(refreshToken);

            return new AuthResponse
            {
                AccountId = account.Id,
                UserId = account.UserId,
                CompanyId = account.CompanyId,
                Email = account.Email,
                AccountType = account.AccountType.ToString(),
                Roles = roles,
                AccessToken = accessToken,
                RefreshToken = refreshTokenValue,
                AccessTokenExpiresAt = accessTokenExpiresAt,
                RefreshTokenExpiresAt = refreshTokenExpiresAt
            };

        }
        private string GenerateAccessToken(Account account, List<string> roles, DateTime expiresAt)
        {
            var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, account.Id.ToString()),
                    new Claim("accountId", account.Id.ToString()),
                    new Claim("email", account.Email),
                    new Claim("accountType", account.AccountType.ToString())
                };
            if (account.UserId.HasValue)
            {
                claims.Add(new Claim("userId", account.UserId.Value.ToString()));
            }

            if (account.CompanyId.HasValue)
            {
                claims.Add(new Claim("companyId", account.CompanyId.Value.ToString()));
            }
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_jwtSettings.Key)
        );

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256
            );

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: expiresAt,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
        private static string GenerateRefreshToken()
        {
            var randomBytes = new byte[64];

            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);

            return Convert.ToBase64String(randomBytes);
        }

        public async Task<ApiResponse<bool>> LogoutAsync(LogoutRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.RefreshToken))
            {
                return ApiResponse<bool>.Fail("Refresh token is required");
            }

            var refreshToken = await _unitOfWork.RefreshTokens.GetByTokenAsync(request.RefreshToken);

            if (refreshToken == null)
            {
                return ApiResponse<bool>.Fail("Invalid refresh token");
            }

            if (refreshToken.IsRevoked)
            {
                return ApiResponse<bool>.Ok(true, "Already logged out");
            }

            refreshToken.IsRevoked = true;
            refreshToken.RevokedAt = DateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync();

            return ApiResponse<bool>.Ok(true, "Logout successful");
        }

        public async Task<ApiResponse<AuthResponse>> RefreshTokenAsync(RefreshTokenRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.RefreshToken))
            {
                return ApiResponse<AuthResponse>.Fail("Refresh token is required");
            }

            var refreshToken = await _unitOfWork.RefreshTokens.GetByTokenAsync(request.RefreshToken);

            if (refreshToken == null)
            {
                return ApiResponse<AuthResponse>.Fail("Invalid refresh token");
            }

            if (refreshToken.IsRevoked)
            {
                return ApiResponse<AuthResponse>.Fail("Refresh token has been revoked");
            }

            if (refreshToken.ExpiredAt <= DateTime.UtcNow)
            {
                return ApiResponse<AuthResponse>.Fail("Refresh token has expired");
            }

            var account = refreshToken.Account;

            if (account == null || account.IsDeleted)
            {
                return ApiResponse<AuthResponse>.Fail("Account not found");
            }

            if (account.Status != AccountStatus.Active)
            {
                return ApiResponse<AuthResponse>.Fail("Account is not active");
            }

            var roles = account.AccountRoles
                .Select(x => x.Role.Name)
                .ToList();

            refreshToken.IsRevoked = true;
            refreshToken.RevokedAt = DateTime.UtcNow;

            var authResponse = await BuildAuthResponseAsync(account, roles);

            await _unitOfWork.SaveChangesAsync();

            return ApiResponse<AuthResponse>.Ok(authResponse, "Token refreshed successfully");
        }

    }

}