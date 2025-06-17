using UdemyNewMicroservice.Shared;

namespace UdemyNewMicroservice.Payment.Api.Feature.GetAllPaymentsByUserId
{
    public record GetAllPaymentsByUserIdQuery : IRequestByServiceResult<List<GetAllPaymentsByUserIdResponse>>;
}