#region

using MediatR;
using Microsoft.EntityFrameworkCore;
using UdemyNewMicroservice.Payment.Api.Repositories;
using UdemyNewMicroservice.Shared;
using UdemyNewMicroservice.Shared.Services;

#endregion

namespace UdemyNewMicroservice.Payment.Api.Feature.Payments.GetAllPaymentsByUserId;

public class GetAllPaymentsByUserIdQueryHandler(AppDbContext context, IIdentityService identityService)
    : IRequestHandler<GetAllPaymentsByUserIdQuery, ServiceResult<List<GetAllPaymentsByUserIdResponse>>>
{
    public async Task<ServiceResult<List<GetAllPaymentsByUserIdResponse>>> Handle(
        GetAllPaymentsByUserIdQuery request,
        CancellationToken cancellationToken)
    {
        var userId = identityService.UserId;

        var payments = await context.Payments
            .Where(x => x.UserId == userId)
            .Select(x => new GetAllPaymentsByUserIdResponse(
                x.Id,
                x.OrderCode,
                x.Amount.ToString("C"), // Format as currency
                x.Created,
                x.Status))
            .ToListAsync(cancellationToken);


        return ServiceResult<List<GetAllPaymentsByUserIdResponse>>.SuccessAsOk(payments);
    }
}