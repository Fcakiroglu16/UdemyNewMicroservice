using MassTransit;
using MediatR;
using System.Net;
using UdemyNewMicroservice.Order.Application.Contracts.Refit.PaymentService;
using UdemyNewMicroservice.Order.Application.Contracts.Repositories;
using UdemyNewMicroservice.Order.Application.Contracts.UnitOfWork;
using UdemyNewMicroservice.Order.Domain.Entities;
using UdemyNewMicroservice.Shared;
using UdemyNewMicroservice.Shared.Services;

namespace UdemyNewMicroservice.Order.Application.UseCases.Orders.CreateOrder;

public class CreateOrderCommandHandler(
    IOrderRepository orderRepository,
    IGenericRepository<int, Address> addressRepository,
    IIdentityService identityService,
    IUnitOfWork unitOfWork,
    IPublishEndpoint publishEndpoint,
    IPaymentService paymentService) : IRequestHandler<CreateOrderCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        if (!request.Items.Any())
            return ServiceResult.Error("Order items not found", "Order must have at least one item",
                HttpStatusCode.BadRequest);


        var newAddress = new Address
        {
            Province = request.Address.Province,
            District = request.Address.District,
            Street = request.Address.Street,
            ZipCode = request.Address.ZipCode,
            Line = request.Address.Line
        };


        var order = Domain.Entities.Order.CreateUnPaidOrder(identityService.UserId, request.DiscountRate,
            newAddress.Id);
        foreach (var orderItem in request.Items)
            order.AddOrderItem(orderItem.ProductId, orderItem.ProductName, orderItem.UnitPrice);


        order.Address = newAddress;


        orderRepository.Add(order);
        await unitOfWork.CommitAsync(cancellationToken);


        CreatePaymentRequest paymentRequest = new CreatePaymentRequest(order.Code, request.Payment.CardNumber,
            request.Payment.CardHolderName, request.Payment.Expiration, request.Payment.Cvc, order.TotalPrice);
        var paymentResponse = await paymentService.CreateAsync(paymentRequest);


        if (paymentResponse.Status == false)
            return ServiceResult.Error(paymentResponse.ErrorMessage!, HttpStatusCode.InternalServerError);


        order.SetPaidStatus(paymentResponse.PaymentId!.Value);

        orderRepository.Update(order);
        await unitOfWork.CommitAsync(cancellationToken);


        await publishEndpoint.Publish(new Bus.Events.OrderCreatedEvent(order.Id, identityService.UserId),
            cancellationToken);
        return ServiceResult.SuccessAsNoContent();
    }
}