#region

using UdemyNewMicroservice.Shared;

#endregion

namespace UdemyNewMicroservice.Basket.Api.Features.Baskets.AddBasketItem;

public record AddBasketItemCommand(Guid CourseId, string CourseName, decimal CoursePrice, string? ImageUrl)
    : IRequestByServiceResult;