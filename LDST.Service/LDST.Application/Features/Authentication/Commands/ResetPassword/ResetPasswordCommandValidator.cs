using FluentValidation;
using LDST.Application.Features.Authentication.Commands.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDST.Application.Features.Authentication.Commands.ResetPassword;
internal sealed class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordCommandValidator()
    {
        RuleFor(x => x.Password)
            .Equal(customer => customer.ConfirmPassword)
            .WithMessage("The password and confirmation password do not match.");
    }
}
