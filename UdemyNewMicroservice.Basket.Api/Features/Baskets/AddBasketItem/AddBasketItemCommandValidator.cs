#region

using FluentValidation;

#endregion

namespace UdemyNewMicroservice.Basket.Api.Features.Baskets.AddBasketItem;

public class AddBasketItemCommandValidator : AbstractValidator<AddBasketItemCommand>
{
    public AddBasketItemCommandValidator()
    {
        RuleFor(x => x.CourseId).NotEmpty().WithMessage("CourseId is required");
        RuleFor(x => x.CourseName).NotEmpty().WithMessage("CourseName is required");
        RuleFor(x => x.CoursePrice).GreaterThan(0).WithMessage("{PropertyName} must be greater than zero");
    }
}