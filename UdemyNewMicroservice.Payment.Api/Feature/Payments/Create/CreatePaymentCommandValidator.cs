using FluentValidation;

namespace UdemyNewMicroservice.Payment.Api.Feature.Payments.Create
{
    public class CreatePaymentCommandValidator : AbstractValidator<CreatePaymentCommand>
    {
        public CreatePaymentCommandValidator()
        {
            RuleFor(x => x.OrderCode).NotEmpty().WithMessage("Order code is required.");
            RuleFor(x => x.CardNumber).NotEmpty().WithMessage("Card number is required.");
            RuleFor(x => x.CardHolderName).NotEmpty().WithMessage("Card holder name is required.");
            RuleFor(x => x.CardExpirationDate).NotEmpty().WithMessage("Card expiration date is required.");
            RuleFor(x => x.CardSecurityNumber).NotEmpty().WithMessage("Card security number is required.");
            RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Amount must be greater than zero.");
        }
    }
}