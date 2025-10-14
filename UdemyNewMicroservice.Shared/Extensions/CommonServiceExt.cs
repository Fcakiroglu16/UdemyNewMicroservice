#region

using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using UdemyNewMicroservice.Shared.ExceptionHandlers;
using UdemyNewMicroservice.Shared.Services;

#endregion

namespace UdemyNewMicroservice.Shared.Extensions;

public static class CommonServiceExt
{
    public static IServiceCollection AddCommonServiceExt(this IServiceCollection services, Type assembly)
    {
        services.AddHttpContextAccessor();
        services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining(assembly));

        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining(assembly);
        services.AddScoped<IIdentityService, IdentityService>();

        services.AddAutoMapper(assembly);
        services.AddExceptionHandler<GlobalExceptionHandler>();
        return services;
    }
}