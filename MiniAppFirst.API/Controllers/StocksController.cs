using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MiniAppFirst.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetStocks()
        {
            var userName = HttpContext.User.Identity.Name;
            var userIdClaims = User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier);

            return Ok($"User : {userName} - {userIdClaims.Value}");
        }
    }
}
