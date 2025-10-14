namespace UdemyNewMicroservice.Order.Domain.Entities;

public enum OrderStatus
{
    WaitingForPayment = 1,
    Paid = 2,
    Cancel = 3
}