using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UdemyNewMicroservice.Order.Application.Contracts.Refit.PaymentService;
using UdemyNewMicroservice.Order.Application.Contracts.Repositories;
using UdemyNewMicroservice.Order.Application.Contracts.UnitOfWork;

namespace UdemyNewMicroservice.Order.Application.BackgroundServices
{
    public class CheckPaymentStatusOrderBackgroundService(IServiceProvider serviceProvider) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = serviceProvider.CreateScope();

            var paymentService = scope.ServiceProvider.GetRequiredService<IPaymentService>();
            var orderRepository = scope.ServiceProvider.GetRequiredService<IOrderRepository>();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            while (!stoppingToken.IsCancellationRequested)
            {
                var orders = orderRepository.Where(x => x.Status == Domain.Entities.OrderStatus.WaitingForPayment)
                    .ToList();

                foreach (var order in orders)
                {
                    var paymentStatusResponse = await paymentService.GetStatusAsync(order.Code);

                    if (paymentStatusResponse.IsPaid!)
                    {
                        await orderRepository.SetStatus(order.Code, paymentStatusResponse.PaymentId!.Value,
                            Domain.Entities.OrderStatus.Paid);
                        await unitOfWork.CommitAsync(stoppingToken);
                    }
                }

                await Task.Delay(2000, stoppingToken);
            }
        }
    }
}