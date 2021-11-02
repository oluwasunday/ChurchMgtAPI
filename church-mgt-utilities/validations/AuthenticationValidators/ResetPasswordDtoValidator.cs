using church_mgt_dtos.AuthenticationDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace church_mgt_utilities.validations.AuthenticationValidators
{
    public class ResetPasswordDtoValidator : AbstractValidator<ResetPasswordDto>
    {
        public ResetPasswordDtoValidator()
        {
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.NewPassword).Password();
            RuleFor(x => x.Token)
                .NotEmpty().WithMessage("Token can not be empty")
                .NotNull().WithMessage("Token can not be null");
        }
    }
}
