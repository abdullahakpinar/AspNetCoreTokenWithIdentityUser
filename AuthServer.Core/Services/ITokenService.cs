using AuthServer.Core.Configurations;
using AuthServer.Core.DataTransferObjects;
using AuthServer.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Core.Services
{
    public interface ITokenService
    {
        TokenDTOs CreateToken(UserApp userApp);
        ClientTokenDTOs CreateTokenByClient(Client client);
    }
}
