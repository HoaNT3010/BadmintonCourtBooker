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

    public class CourtScheduleCreateValidator : AbstractValidator<CourtScheduleCreateRequest>
    {
        public CourtScheduleCreateValidator()
        {
            RuleFor(r => r.Schedules).NotNull().WithMessage("Schedules list cannot be null!")
                .NotEmpty().WithMessage("Schedules list cannot be empty!")
                .Must(r => r.GroupBy(s => s.DayOfWeek).All(group => group.Count() == 1)).WithMessage("Schedules list cannot contains duplicated days of week.");

            RuleForEach(r => r.Schedules).SetValidator(new CourtScheduleValidator());
        }
    }

    public class CourtScheduleValidator : AbstractValidator<CourtScheduleCreate>
    {
        public CourtScheduleValidator()
        {
            RuleFor(s => s.DayOfWeek).IsInEnum().WithMessage("Day of week must be a valid day!");

            RuleFor(s => s.OpenTime).NotEmpty().WithMessage("Open time cannot be empty")
                .Matches(@"^(?:[01]\d|2[0-3]):[0-5]\d:[0-5]\d$").WithMessage("Open time must match the format hh:mm:ss (ex 01:30:00)");

            RuleFor(s => s.CloseTime).NotEmpty().WithMessage("Close time cannot be empty")
                .Matches(@"^(?:[01]\d|2[0-3]):[0-5]\d:[0-5]\d$").WithMessage("Close time must match the format hh:mm:ss (ex 20:30:00)");

        }
    }
}
