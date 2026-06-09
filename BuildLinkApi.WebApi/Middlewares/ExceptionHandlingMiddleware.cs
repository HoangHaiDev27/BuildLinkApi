using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using BuildLinkApi.Application.Common;
using BuildLinkApi.Infrastructure.Data.Migrations;
using Microsoft.AspNetCore.Diagnostics;

namespace BuildLinkApi.WebApi.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate request, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = request;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred");
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = ApiResponse<object>.Fail(
                    "Internal Server Error!",
                    new List<string> { ex.Message }
                );

                var json = JsonSerializer.Serialize(response);

                await context.Response.WriteAsync(json);
            }
        }
    }
}