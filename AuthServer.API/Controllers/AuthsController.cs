using AuthServer.Core.DataTransferObjects;
using AuthServer.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthsController : CustomBaseController
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthsController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateToken(LoginDTOs loginDTOs)
        {
            var result = await _authenticationService.CreateTokenAsync(loginDTOs);
            return ActionResultInstance(result);
        }

        [HttpPost]
        public IActionResult CreateTokenByClient(ClientLoginDTOs clientLoginDTOs)
        {
            var result = _authenticationService.CreateTokenByClient(clientLoginDTOs);

            return ActionResultInstance(result);
        }

        [HttpPost]
        public async Task<IActionResult> RevokeRefreshToken(RefreshTokenDTOs refreshTokenDTOs)
        {
            var result = await _authenticationService.RevokeRefrestTokenAsync(refreshTokenDTOs.Token);

            return ActionResultInstance(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTokenByRefreshToken(RefreshTokenDTOs refreshTokenDTOs)
        {
            var result = await _authenticationService.CreateTokenByRefreshTokenAsync(refreshTokenDTOs.Token);

            return ActionResultInstance(result);
        }
    }
}
