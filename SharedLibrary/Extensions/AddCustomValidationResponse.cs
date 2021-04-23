using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SharedLibrary.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Extensions
{
    public static class AddCustomValidationResponse
    {
        public static void UseCustomValidationResponse(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState.Values.Where(ex => ex.Errors.Count > 0).SelectMany(ex => ex.Errors).Select(ex => ex.ErrorMessage);

                    ErrorDTOs errorDTOs = new ErrorDTOs(errors.ToList(), true);

                    var response = Response<NoContentResult>.Fail(errorDTOs, 400);
                    return new BadRequestObjectResult(response);
                };
            });
        }
    }
}
