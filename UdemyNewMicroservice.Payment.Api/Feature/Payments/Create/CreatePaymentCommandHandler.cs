#region

using System.Net;
using MediatR;
using UdemyNewMicroservice.Payment.Api.Repositories;
using UdemyNewMicroservice.Shared;
using UdemyNewMicroservice.Shared.Services;

#endregion

namespace UdemyNewMicroservice.Payment.Api.Feature.Payments.Create;

public class CreatePaymentCommandHandler(
    AppDbContext appDbContext,
    IIdentityService identityService,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<CreatePaymentCommand, ServiceResult<CreatePaymentResponse>>
{
    public async Task<ServiceResult<CreatePaymentResponse>> Handle(CreatePaymentCommand request,
        CancellationToken cancellationToken)
    {
        var userId = identityService.UserId;
        var userName = identityService.UserName;
        var roles = identityService.Roles;


        var (isSuccess, errorMessage) = await ExternalPaymentProcessAsync(request.CardNumber,
            request.CardHolderName,
            request.CardExpirationDate, request.CardSecurityNumber, request.Amount);


        if (!isSuccess)
            return ServiceResult<CreatePaymentResponse>.Error("Payment Failed", errorMessage!,
                HttpStatusCode.BadRequest);


        var newPayment = new Repositories.Payment(userId, request.OrderCode, request.Amount);
        newPayment.SetStatus(PaymentStatus.Success);

        appDbContext.Payments.Add(newPayment);
        await appDbContext.SaveChangesAsync(cancellationToken);

        return ServiceResult<CreatePaymentResponse>.SuccessAsOk(
            new CreatePaymentResponse(newPayment.Id, true, null));
    }


    private async Task<(bool isSuccess, string? errorMessage)> ExternalPaymentProcessAsync(string cardNumber,
        string cardHolderName, string cardExpirationDate, string cardSecurityNumber, decimal amount)
    {
        // Simulate external payment processing logic
        await Task.Delay(1000); // Simulating a delay for the external service call
        return (true, null); // Assume the payment was successful

        //return (false,"Payment failed due to insufficient funds."); // Simulate a failure case
    }
}