using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Runtime.Internal.Util;
using BuildLinkApi.Application.Interfaces.Services;
using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using BuildLinkApi.Application.DTOs;

namespace BuildLinkApi.Infrastructure.Services
{
    public class TurnstileService : ITurnstileService
    {
        private const string VerifyUrl =
            "https://challenges.cloudflare.com/turnstile/v0/siteverify";

        private readonly HttpClient _http;
        private readonly string _secretKey;
        private readonly ILogger<TurnstileService> _logger;

        public TurnstileService(
            HttpClient http,
            IConfiguration configuration,
            ILogger<TurnstileService> logger)
        {
            _http = http;
            _secretKey = configuration["Turnstile:SecretKey"] ?? string.Empty;
            _logger = logger;
        }
        public async Task<bool> VerifyAsync(string token, string? remoteIp)
        {
            if (string.IsNullOrWhiteSpace(token) || string.IsNullOrWhiteSpace(_secretKey))
                return false;

            var form = new Dictionary<string, string>
            {
                ["secret"] = _secretKey,
                ["response"] = token,
            };
            if (!string.IsNullOrWhiteSpace(remoteIp))
                form["remoteip"] = remoteIp;

            try
            {
                var response = await _http.PostAsync(VerifyUrl, new FormUrlEncodedContent(form));
                var result = await response.Content.ReadFromJsonAsync<TurnstileVerifyResponse>();
                return result?.Success == true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Turnstile verification failed");
                return false; // fail-closed: lỗi mạng coi như không qua
            }
        }
    }
}