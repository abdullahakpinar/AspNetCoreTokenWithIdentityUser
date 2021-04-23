using AuthServer.Core.DataTransferObjects;
using SharedLibrary.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Core.Services
{
    public interface IAuthenticationService
    {
        Task<Response<TokenDTOs>> CreateTokenAsync(LoginDTOs loginDTOs);
        Task<Response<TokenDTOs>> CreateTokenByRefreshTokenAsync(string refreshToken);
        Task<Response<NoDataDTOs>> RevokeRefrestTokenAsync(string refreshToken);
        Response<ClientTokenDTOs> CreateTokenByClient(ClientLoginDTOs clientLoginDTOs);
    }
}
