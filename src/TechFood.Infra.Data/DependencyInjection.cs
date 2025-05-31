using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TechFood.Application.QueryProvider;
using TechFood.Domain.Repositories;
using TechFood.Domain.Shared.Interfaces;
using TechFood.Domain.UoW;
using TechFood.Infra.Data.Contexts;
using TechFood.Infra.Data.QueryProviders;
using TechFood.Infra.Data.Repositories;
using TechFood.Infra.Data.UoW;

namespace TechFood.Infra.Data;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraData(this IServiceCollection services)
    {
        //Context
        services.AddScoped<TechFoodContext>();
        services.AddDbContext<TechFoodContext>((serviceProvider, options) =>
        {
            var config = serviceProvider.GetRequiredService<IConfiguration>();

            options.UseSqlServer(config.GetConnectionString("DataBaseConection"));
        });

        //UoW
        services.AddScoped<IUnitOfWorkTransaction, UnitOfWorkTransaction>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUnitOfWork, AnotherUnitOfWork>();

        //Data
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPreparationRepository, PreparationRepository>();

        //Queries
        services.AddScoped<IProductQueryProvider, ProductQueryProvider>();
        services.AddScoped<ICategoryQueryProvider, CategoryQueryProvider>();
        services.AddScoped<ICustomerQueryProvider, CustomerQueryProvider>();
        services.AddScoped<IPreparationQueryProvider, PreparationQueryProvider>();
        services.AddScoped<IMenuQueryProvider, MenuQueryProvider>();

        //DomainEvents
        services.AddScoped<IDomainEventStore, TechFoodEventsContext>();

        return services;
    }
}
