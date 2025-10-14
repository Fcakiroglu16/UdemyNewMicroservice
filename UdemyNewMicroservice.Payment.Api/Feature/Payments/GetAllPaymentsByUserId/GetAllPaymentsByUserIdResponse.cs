#region

using UdemyNewMicroservice.Payment.Api.Repositories;

#endregion

namespace UdemyNewMicroservice.Payment.Api.Feature.Payments.GetAllPaymentsByUserId;

public record GetAllPaymentsByUserIdResponse(
    Guid Id,
    string OrderCode,
    string Amount,
    DateTime Created,
    PaymentStatus Status);