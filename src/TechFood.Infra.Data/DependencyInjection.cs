using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TechFood.Application.Categories.Queries;
using TechFood.Application.Customers.Queries;
using TechFood.Application.Menu.Queries;
using TechFood.Application.Orders.Queries;
using TechFood.Application.Preparations.Queries;
using TechFood.Application.Products.Queries;
using TechFood.Domain.Repositories;
using TechFood.Domain.Shared.Interfaces;
using TechFood.Domain.UoW;
using TechFood.Infra.Data.Contexts;
using TechFood.Infra.Data.Queries;
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
        services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<TechFoodContext>());

        //DomainEvents
        services.AddScoped<IDomainEventStore>(serviceProvider => serviceProvider.GetRequiredService<TechFoodContext>());

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
        services.AddScoped<IOrderQueryProvider, OrderQueryProvider>();

        return services;
    }
}
