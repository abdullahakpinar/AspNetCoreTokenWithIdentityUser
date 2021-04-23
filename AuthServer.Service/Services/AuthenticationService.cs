using AuthServer.Core.Configurations;
using AuthServer.Core.DataTransferObjects;
using AuthServer.Core.Entities;
using AuthServer.Core.Repositories;
using AuthServer.Core.Services;
using AuthServer.Core.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SharedLibrary.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.Service.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly List<Client> _clients;
        private readonly ITokenService _tokenService;
        private readonly UserManager<UserApp> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<UserRefreshToken> _userRefreshTokenRepository;

        public AuthenticationService(IOptions<List<Client>> optionsClient, ITokenService tokenService, UserManager<UserApp> userManager, IUnitOfWork unitOfWork, IGenericRepository<UserRefreshToken> userRefreshTokenRepository)
        {
            _clients = optionsClient.Value;
            _tokenService = tokenService;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _userRefreshTokenRepository = userRefreshTokenRepository;
        }

        public async Task<Response<TokenDTOs>> CreateTokenAsync(LoginDTOs loginDTOs)
        {
            if (loginDTOs == null)
            {
                throw new ArgumentNullException(nameof(loginDTOs));
            }
            var user = await _userManager.FindByEmailAsync(loginDTOs.Email);
            if (user == null)
            {
                return Response<TokenDTOs>.Fail("Email or Password is wrong", 400, true);
            }
            if (!await _userManager.CheckPasswordAsync(user, loginDTOs.Password)) 
            {
                return Response<TokenDTOs>.Fail("Email or Password is wrong", 400, true);
            }
            var token = _tokenService.CreateToken(user);
            var userRefreshToken = await _userRefreshTokenRepository.Where(refk => refk.UserId == user.Id).SingleOrDefaultAsync();
            if (userRefreshToken == null)
            {
                await _userRefreshTokenRepository.AddAsync(new UserRefreshToken { UserId = user.Id, Code = token.RefreshToken, Expiration = token.RefreshTokenExpiration });
            }
            else
            {
                userRefreshToken.Code = token.RefreshToken;
                userRefreshToken.Expiration = token.RefreshTokenExpiration;
            }

            await _unitOfWork.CommitAsync();
            return Response<TokenDTOs>.Success(token, 200);
        }

        public Response<ClientTokenDTOs> CreateTokenByClient(ClientLoginDTOs clientLoginDTOs)
        {
            var client = _clients.SingleOrDefault(c => c.Id == clientLoginDTOs.Id && c.Secret == clientLoginDTOs.Secret);
            if (client == null)
            {
                return Response<ClientTokenDTOs>.Fail("ClientId or ClientSecret not found", 404, true);
            }
            var token = _tokenService.CreateTokenByClient(client);
            return Response<ClientTokenDTOs>.Success(token, 200);
        }

        public async Task<Response<TokenDTOs>> CreateTokenByRefreshTokenAsync(string refreshToken)
        {
            var refToken = await _userRefreshTokenRepository.Where(reft => reft.Code == refreshToken).SingleOrDefaultAsync();
            if (refToken == null)
            {
                return Response<TokenDTOs>.Fail("Refresh token not found.", 404, true);
            }
            var user = await _userManager.FindByIdAsync(refToken.UserId);
            if (user == null)
            {
                return Response<TokenDTOs>.Fail("UserId not found.", 404, true);
            }
            var tokenDTO = _tokenService.CreateToken(user);

            refToken.Code = tokenDTO.RefreshToken;
            refToken.Expiration = tokenDTO.RefreshTokenExpiration;
            await _unitOfWork.CommitAsync();
            return Response<TokenDTOs>.Success(tokenDTO, 200);
        }

        public async Task<Response<NoDataDTOs>> RevokeRefrestTokenAsync(string refreshToken)
        {
            var refToken = await _userRefreshTokenRepository.Where(refT => refT.Code == refreshToken).SingleOrDefaultAsync();

            if (refToken == null)
            {
                return Response<NoDataDTOs>.Fail("Refresh token not found.", 404, true);
            }
            _userRefreshTokenRepository.Remove(refToken);
            await _unitOfWork.CommitAsync();

            return Response<NoDataDTOs>.Success(200);
        }
    }
}
