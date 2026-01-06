using CatAdoption.Domain.Interfaces;
using CatAdoption.Infrastructure.Data;
using CatAdoption.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CatAdoption.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AdoptionDbContext>(options => options.UseSqlite(connectionString));
        // services.AddDbContext<AdoptionDbContext>(options => options.UseSqlServer(connectionString));
        
        // AddScoped means - Create one instance of the class per request
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        return services;
    }
}