using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Myrtex.Application.Interfaces;

namespace Myrtex.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection
        services, IConfiguration configuration)
    {
        var connectionString = configuration["DbConnection"];
        services.AddDbContext<MyrtexDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
        services.AddScoped<IMyrtexDbContext>(provider =>
            provider.GetRequiredService<MyrtexDbContext>());

        return services;
    }
}
