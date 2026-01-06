using Microsoft.Extensions.DependencyInjection;
using CatAdoption.Application.Services;

namespace CatAdoption.Application;

public static class DependencyInjection
{
    // inject our services
    // installer for our client side to have access to our services
    // application layer to register its own services
    // reason for this is client shouldn't need to know exactly which services exist,
    // only what application decides they need to know
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // creates one instance of the service per user request
        // the instance stays within memory until the use request ends or while the repositories are running
        services.AddScoped<AdoptionService>();

        return services;
    }
}