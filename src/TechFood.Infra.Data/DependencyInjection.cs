using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TechFood.Domain.Repositories;
using TechFood.Domain.UoW;
using TechFood.Infra.Data.Contexts;
using TechFood.Infra.Data.Repositories;
using TechFood.Infra.Data.UoW;

namespace TechFood.Infra.Data;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraData(this IServiceCollection services)
    {
        services.AddScoped<TechFoodContext>();
        services.AddDbContext<TechFoodContext>((serviceProvider, options) =>
        {
            var config = serviceProvider.GetRequiredService<IConfiguration>();

            options.UseSqlServer(config.GetConnectionString("DataBaseConection"));
        });

        services.AddScoped<IUnitOfWorkTransaction, UnitOfWorkTransaction>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUnitOfWork, AnotherUnitOfWork>();

        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();

        return services;
    }
}
