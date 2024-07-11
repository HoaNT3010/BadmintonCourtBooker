using Application.RequestDTOs.Booking;
using Application.Utilities;
using FluentValidation;

namespace Application.Validators.Booking
{
    public class CreateBookingRequestValidator : AbstractValidator<CreateBookingRequest>
    {
        public CreateBookingRequestValidator()
        {
            RuleFor(c => c.RentDate)
                .NotEmpty().WithMessage("Rent date cannot be empty!")
                .Must(value => DateTimeHelper.ConvertDateString(value) != null).WithMessage("Rent date must be a valid date follow the format dd/MM/yyyy (ex: 08/07/2024)");

            RuleFor(c => c.SlotId)
                .GreaterThan(0).WithMessage("ID of court's slot must be larger than 0!");

            RuleFor(c => c.BookingMethodId)
                .GreaterThan(0).WithMessage("ID of court's booking method must be larger than 0!");

            RuleFor(c => c.PaymentMethodId)
                .GreaterThan(0).WithMessage("ID of court's payment method must be larger than 0!");
        }
    }

    public class CreateMultipleBookingRequestValidator : AbstractValidator<CreateMultipleBookingRequest>
    {
        public CreateMultipleBookingRequestValidator()
        {
            RuleFor(c => c.SlotId)
                .GreaterThan(0).WithMessage("ID of court's slot must be larger than 0!");

            RuleFor(c => c.BookingMethodId)
                .GreaterThan(0).WithMessage("ID of court's booking method must be larger than 0!");

            RuleFor(c => c.PaymentMethodId)
                .GreaterThan(0).WithMessage("ID of court's payment method must be larger than 0!");

            RuleFor(c => c.BookingMethodId)
                .GreaterThan(0).WithMessage("ID of court's booking method must be larger than 0!");

            RuleFor(c => c.RentDates)
                .NotNull().WithMessage("List of rent dates for booking cannot be null!")
                .NotEmpty().WithMessage("List of rent dates for booking cannot be empty!")
                .Must(value => value.Count >= 4).WithMessage("Fixed booking must contains at least 4 rent dates!")
                .Must(value => value.GroupBy(i => i.RentDate).All(group => group.Count() == 1)).WithMessage("Rent dates cannot be duplicated");

            RuleForEach(c => c.RentDates)
                .SetValidator(new BookingRentDateValidator());
        }
    }

    public class BookingRentDateValidator : AbstractValidator<BookingRentDate>
    {
        public BookingRentDateValidator()
        {
            RuleFor(c => c.RentDate)
                .NotEmpty().WithMessage("Rent date cannot be empty!")
                .Must(value => DateTimeHelper.ConvertDateString(value) != null).WithMessage("Rent date must be a valid date follow the format dd/MM/yyyy (ex: 08/07/2024)");
        }
    }
}
