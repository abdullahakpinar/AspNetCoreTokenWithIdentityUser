using AuthServer.Core.DataTransferObjects;
using AuthServer.Core.Entities;
using AuthServer.Core.Services;
using Microsoft.AspNetCore.Identity;
using SharedLibrary.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Service.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<UserApp> _userManager;

        public UserService(UserManager<UserApp> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Response<UserAppDTOs>> CreateUserAsync(CreateUserDTOs createUserDTOs)
        {
            var user = new UserApp { Email = createUserDTOs.Email, UserName = createUserDTOs.UserName };
            var result = await _userManager.CreateAsync(user, createUserDTOs.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return Response<UserAppDTOs>.Fail(new ErrorDTOs(errors, true), 400);
            }
            return Response<UserAppDTOs>.Success(ObjectMapper.Mapper.Map<UserAppDTOs>(user), 200);
        }

        public async Task<Response<UserAppDTOs>> GetUserByNameAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return Response<UserAppDTOs>.Fail("UserName not found.", 404, true);
            }
            return Response<UserAppDTOs>.Success(ObjectMapper.Mapper.Map<UserAppDTOs>(user), 200);
        }
    }
}
