using church_mgt_dtos.DepartmentDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace church_mgt_utilities.validations.DepartmentValidators
{
    public class AddDepartmentDtoValidator : AbstractValidator<AddDepartmentDto>
    {
        public AddDepartmentDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("Department name is required")
                .NotEmpty().WithMessage("Department name is required");
            RuleFor(x => x.Description)
                .NotNull().WithMessage("Department description is required")
                .NotEmpty().WithMessage("Department description is required");
        }
    }
}
