using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BuildLinkApi.Application.DTOs
{
    public class TurnstileVerifyResponse
    {
        [JsonPropertyName("success")] public bool Success { get; set; }
        [JsonPropertyName("error-codes")] public string[]? ErrorCodes { get; set; }
    }
}
