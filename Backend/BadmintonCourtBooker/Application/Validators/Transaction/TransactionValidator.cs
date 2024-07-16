using Application.RequestDTOs.Transaction;
using FluentValidation;

namespace Application.Validators.Transaction
{
    public class BookingTimeRechargeRequestValidator : AbstractValidator<BookingTimeRechargeRequest>
    {
        public BookingTimeRechargeRequestValidator()
        {
            RuleFor(e => e.RechargeAmount).GreaterThan(0).WithMessage("Recharge amount must be greater than 0")
                            .Must(value => Math.Round(value, 1) == value).WithMessage("Recharge amount can has at most 1 decimal place");
        }
    }
}
