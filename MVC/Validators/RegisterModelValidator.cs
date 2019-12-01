using FluentValidation;
using MVC.Models;

namespace MVC.Validators
{
    public class RegisterModelValidator : AbstractValidator<RegisterModel>
    {
        public RegisterModelValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Please enter a correct format of email");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
            RuleFor(x => x.Password).MinimumLength(5).WithMessage("Minimum length of password is 5 charachters");
            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Please confirm password");

            RuleFor(x => x.Password).Equal(x => x.ConfirmPassword)
                .When(x => !string.IsNullOrEmpty(x.ConfirmPassword) && !string.IsNullOrEmpty(x.Password))
                .WithMessage("Passwords should be the same");
        }
    }
}
