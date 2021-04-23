using AuthServer.Core.DataTransferObjects;
using SharedLibrary.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Core.Services
{
    public interface IUserService
    {
        Task<Response<UserAppDTOs>> CreateUserAsync(CreateUserDTOs createUserDTOs);
        Task<Response<UserAppDTOs>> GetUserByNameAsync(string userName);
    }
}
