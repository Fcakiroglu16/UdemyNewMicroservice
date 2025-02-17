﻿using Asp.Versioning.Builder;
using UdemyNewMicroservice.Discount.Api.Features.Discounts.CreateDiscount;


namespace UdemyNewMicroservice.Catalog.Api.Features.Courses
{
    public static class DiscountEndpointExt
    {
        public static void AddDiscountGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/discounts").WithTags("discounts").WithApiVersionSet(apiVersionSet)
                .CreateDiscountGroupItemEndpoint()
                .GetDiscountByCodeGroupItemEndpoint();
        }
    }
}