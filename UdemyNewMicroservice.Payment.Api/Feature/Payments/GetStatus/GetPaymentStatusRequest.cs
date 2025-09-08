using UdemyNewMicroservice.Shared;

namespace UdemyNewMicroservice.Payment.Api.Feature.Payments.GetStatus;

public record GetPaymentStatusRequest(string orderCode) : IRequestByServiceResult<GetPaymentStatusResponse>;