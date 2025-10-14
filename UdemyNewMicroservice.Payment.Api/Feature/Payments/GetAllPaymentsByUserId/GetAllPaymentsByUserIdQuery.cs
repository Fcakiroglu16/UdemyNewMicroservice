#region

using UdemyNewMicroservice.Shared;

#endregion

namespace UdemyNewMicroservice.Payment.Api.Feature.Payments.GetAllPaymentsByUserId;

public record GetAllPaymentsByUserIdQuery : IRequestByServiceResult<List<GetAllPaymentsByUserIdResponse>>;