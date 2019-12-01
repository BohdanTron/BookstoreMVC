using FluentValidation;
using MVC.Models;

namespace MVC.Validators
{
    public class LoginModelValidator : AbstractValidator<LoginModel>
    {
        public LoginModelValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Please enter a correct format of email");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
        }
    }
}
