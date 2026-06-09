using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildLinkApi.Application.Common;
using BuildLinkApi.Application.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BuildLinkApi.WebApi
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public TestController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("roles")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _unitOfWork.Roles.GetAllAsync();

            return Ok(ApiResponse<object>.Ok(roles, "Roles loaded successfully"));
        }
    }
}