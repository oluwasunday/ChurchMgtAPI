using church_mgt_dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace church_mgt_utilities.validations.AuthenticationValidators
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.Address)
                .NotNull().WithMessage("Address field is required")
                .NotEmpty().WithMessage("Address field is required");
            RuleFor(x => x.Password).Password();
            RuleFor(x => x.PhoneNumber).PhoneNumber();
            RuleFor(x => x.FirstName).HumanName();
            RuleFor(x => x.LastName).HumanName();
            RuleFor(x => x.MiddleName).HumanName();
            RuleFor(x => x.Email).EmailAddress().WithMessage("Enter valid email address");
            RuleFor(x => x.Occupation)
                .NotNull().WithMessage("Occupation field is required")
                .NotEmpty().WithMessage("Occupation field is required");
            RuleFor(x => x.MaritalStatus)
                .NotNull().WithMessage("Marital status field is required")
                .NotEmpty().WithMessage("Marital status field is required");
            RuleFor(x => x.Gender)
                .NotNull().WithMessage("Gender field is required")
                .NotEmpty().WithMessage("Gender field is required");
            RuleFor(x => x.Title)
                .NotNull().WithMessage("Title field is required")
                .NotEmpty().WithMessage("Title field is required");
        }
    }
}
