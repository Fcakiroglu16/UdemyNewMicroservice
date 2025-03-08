using UdemyNewMicroservice.Order.Application.Contracts.Repositories;

namespace UdemyNewMicroservice.Order.Persistence.Repositories;

public class OrderRepository(AppDbContext context) : GenericRepository<Guid, Domain.Entities.Order>(context), IOrderRepository
{
}