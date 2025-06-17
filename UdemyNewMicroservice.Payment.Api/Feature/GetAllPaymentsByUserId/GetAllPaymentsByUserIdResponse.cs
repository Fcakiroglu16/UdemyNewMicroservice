using UdemyNewMicroservice.Payment.Api.Repositories;

namespace UdemyNewMicroservice.Payment.Api.Feature.GetAllPaymentsByUserId
{
    public record GetAllPaymentsByUserIdResponse(
        Guid Id,
        string OrderCode,
        string Amount,
        DateTime Created,
        PaymentStatus Status);
}