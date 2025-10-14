#region

using Asp.Versioning.Builder;
using UdemyNewMicroservice.Payment.Api.Feature.Payments.Create;
using UdemyNewMicroservice.Payment.Api.Feature.Payments.GetAllPaymentsByUserId;
using UdemyNewMicroservice.Payment.Api.Feature.Payments.GetStatus;

#endregion

namespace UdemyNewMicroservice.Payment.Api.Feature.Payments;

public static class PaymentEndpointExt
{
    public static void AddPaymentGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
    {
        app.MapGroup("api/v{version:apiVersion}/payments").WithTags("payments").WithApiVersionSet(apiVersionSet)
            .CreatePaymentGroupItemEndpoint().GetAllPaymentsByUserIdGroupItemEndpoint()
            .GetPaymentStatusGroupItemEndpoint();
    }
}