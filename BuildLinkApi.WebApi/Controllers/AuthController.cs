using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildLinkApi.Application.Common;
using BuildLinkApi.Application.DTOs;
using BuildLinkApi.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuildLinkApi.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authservice;
        public AuthController(IAuthService authService)
        {
            _authservice = authService;
        }
        [HttpPost("me")]
        public async Task<IActionResult> GetCurrentAccount([FromBody] Guid accountId)
        {
            var user = await _authservice.GetCurrentAccountAsync(accountId);
            if (user == null)
            {
                return BadRequest(user);
            }
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _authservice.LoginAsync(request);

            if (!result.Success || result.Data == null)
            {
                return BadRequest(result);
            }
            Response.Cookies.Append("access_token", result.Data.AccessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = false, // Production đổi thành true
                SameSite = SameSiteMode.Lax,
                Expires = result.Data.AccessTokenExpiresAt
            });

            Response.Cookies.Append("refresh_token", result.Data.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = false, // Production đổi thành true
                SameSite = SameSiteMode.Lax,
                Expires = result.Data.RefreshTokenExpiresAt
            });

            // result.Data.AccessToken = string.Empty;
            // result.Data.RefreshToken = string.Empty;
            return Ok(result);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var result = await _authservice.RegisterAsync(request);
            if (!result.Success || result.Data == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refresh_token"];
            if (string.IsNullOrWhiteSpace(refreshToken))
            {
                return Unauthorized(
                    ApiResponse<object>.Fail("Refresh token not found")
                );
            }
            var request = new RefreshTokenRequest
            {
                RefreshToken = refreshToken!
            };
            var result = await _authservice.RefreshTokenAsync(request);

            if (!result.Success || result.Data == null)
            {
                return Unauthorized(result);
            }

            Response.Cookies.Append(
                "access_token",
                result.Data.AccessToken,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = false, // true khi deploy HTTPS
                    SameSite = SameSiteMode.Strict,
                    Expires = result.Data.AccessTokenExpiresAt
                });

            Response.Cookies.Append(
                "refresh_token",
                result.Data.RefreshToken,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = false,
                    SameSite = SameSiteMode.Strict,
                    Expires = result.Data.RefreshTokenExpiresAt
                });

            result.Data.AccessToken = string.Empty;
            result.Data.RefreshToken = string.Empty;

            return Ok(result);
        }
        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var refreshToken = Request.Cookies["refresh_token"];

            if (!string.IsNullOrWhiteSpace(refreshToken))
            {
                var refresh = new LogoutRequest
                {
                    RefreshToken = refreshToken
                };
                await _authservice.LogoutAsync(refresh);
            }

            Response.Cookies.Delete("access_token");

            Response.Cookies.Delete("refresh_token");

            return Ok(
                ApiResponse<bool>.Ok(
                    true,
                    "Logout successful"
                ));
        }
    }

}