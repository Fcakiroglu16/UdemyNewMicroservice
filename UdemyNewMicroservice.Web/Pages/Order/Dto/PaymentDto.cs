namespace UdemyNewMicroservice.Web.Pages.Order.Dto;

public record PaymentDto(string CardNumber, string CardHolderName, string Expiration, string Cvc, decimal Amount);