using Application.RequestDTOs.Court;
using Domain.Enums;
using FluentValidation;

namespace Application.Validators.Court
{
    public class CourtCreateValidator : AbstractValidator<CourtCreateRequest>
    {
        public CourtCreateValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Name cannot be empty!")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters!");

            RuleFor(c => c.Description)
                .NotEmpty().WithMessage("Description cannot be empty!")
                .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters!");

            RuleFor(c => c.PhoneNumber)
                .NotEmpty().WithMessage("Phone number cannot be empty!")
                .MaximumLength(15).WithMessage("Phone number cannot exceed 15 characters!")
                .Matches(@"^\d+$").WithMessage("Phone number must contain number only!");

            RuleFor(c => c.Address)
                .NotEmpty().WithMessage("Address cannot be empty!")
                .MaximumLength(200).WithMessage("Address cannot exceed 200 characters!");

            RuleFor(c => c.CourtType)
                .NotEqual(CourtType.None).WithMessage("Court type cannot be None!")
                .IsInEnum().WithMessage("Court type must be a valid value!");

            RuleFor(c => c.SlotType)
                .NotEqual(SlotType.None).WithMessage("Slot type cannot be None!")
                .IsInEnum().WithMessage("Slot type must be a valid value!");
        }
    }
}
