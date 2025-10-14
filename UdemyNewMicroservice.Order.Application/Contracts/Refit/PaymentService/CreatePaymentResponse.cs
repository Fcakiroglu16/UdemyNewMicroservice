namespace UdemyNewMicroservice.Order.Application.Contracts.Refit.PaymentService;

public record CreatePaymentResponse(Guid? PaymentId, bool Status, string? ErrorMessage);