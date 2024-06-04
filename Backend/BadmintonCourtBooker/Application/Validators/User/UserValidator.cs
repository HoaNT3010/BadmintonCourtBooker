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
                .EmailAddress().WithMessage("Email address is not valid!")
                .MaximumLength(254).WithMessage("Email address cannot exceed 254 characters!");

            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("Password cannot be empty!");
        }
    }

    public class UserRegisterValidator : AbstractValidator<CustomerRegisterRequest>
    {
        public UserRegisterValidator()
        {
            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Email address cannot be empty!")
                .EmailAddress().WithMessage("Email address is not valid!")
                .MaximumLength(254).WithMessage("Email address cannot exceed 254 characters!");

            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("Password cannot be empty!")
                .MinimumLength(6).WithMessage("Password length must be at least 6 characters!")
                .MaximumLength(32).WithMessage("Password length cannot exceed 32 characters!");

            RuleFor(u => u.FirstName)
                .NotEmpty().WithMessage("First name cannot be empty!")
                .MaximumLength(50).WithMessage("First name cannot exceed 50 characters!");

            RuleFor(u => u.LastName)
                .NotEmpty().WithMessage("First name cannot be empty!")
                .MaximumLength(50).WithMessage("First name cannot exceed 50 characters!");

            RuleFor(u => u.PhoneNumber)
                .NotEmpty().WithMessage("Phone number cannot be empty!")
                .MaximumLength(15).WithMessage("Phone number cannot exceed 15 characters!")
                .Matches(@"^\d+$").WithMessage("Phone number must contain number only!");
        }
    }
}
