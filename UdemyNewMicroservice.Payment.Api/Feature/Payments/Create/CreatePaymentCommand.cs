using MediatR;
using UdemyNewMicroservice.Shared;

namespace UdemyNewMicroservice.Payment.Api.Feature.Payments.Create
{
    public record CreatePaymentCommand(
        string OrderCode,
        string CardNumber,
        string CardHolderName,
        string CardExpirationDate,
        string CardSecurityNumber,
        decimal Amount) : IRequestByServiceResult<Guid>;
}