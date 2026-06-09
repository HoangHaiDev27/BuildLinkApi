using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildLinkApi.Application.Common;
using Microsoft.AspNetCore.Mvc;

namespace BuildLinkApi.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var result = new
            {
                service = "BuildLink API",
                status = "Running",
                timestamp = DateTime.UtcNow
            };

            return Ok(ApiResponse<object>.Ok(result, "BuildLink API is running"));
        }
    }
}