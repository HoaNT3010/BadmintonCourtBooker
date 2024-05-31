using Application.RequestDTOs.Auth;
using FluentValidation;

namespace Application.Validators.User
{
    public class UserLoginValidator : AbstractValidator<UserLoginRequest>
    {
        public UserLoginValidator()
        {
            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Email address cannot be empty!")
                .EmailAddress().WithMessage("Email address is not valid!");

            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("Password cannot be empty!");
        }
    }
}
