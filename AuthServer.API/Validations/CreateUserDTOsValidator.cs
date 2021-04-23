using AuthServer.Core.DataTransferObjects;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.API.Validations
{
    public class CreateUserDTOsValidator : AbstractValidator<CreateUserDTOs>
    {
        public CreateUserDTOsValidator()
        {
            RuleFor(u => u.Email).NotEmpty().WithMessage("Email is required.").EmailAddress().WithMessage("Email is wrong.");
            RuleFor(u => u.Password).NotEmpty().WithMessage("Password is required.");
            RuleFor(u => u.UserName).NotEmpty().WithMessage("UserName is required.");
        }
    }
}
