using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TechFoodClean.Application.Interfaces.DataSource;
using TechFoodClean.Application.Interfaces.Presenter;
using TechFoodClean.Infrastructure.Data.Contexts;
using TechFoodClean.Infrastructure.Data.Repositories;
using TechFoodClean.Infrastructure.Data.UoW;

namespace TechFoodClean.Infrastructure.Data;

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
        services.AddScoped<IUnitOfWorkTransactionDataSource, UnitOfWorkTransaction>();
        services.AddScoped<IUnitOfWorkDataSource, UnitOfWork>();
        services.AddScoped<IUnitOfWorkDataSource, AnotherUnitOfWork>();

        //Data
        services.AddScoped<ICategoryDataSource, CategoryRepository>();
        services.AddTransient<IImageUrlResolver, ImageUrlResolver>();
        services.AddScoped<IProductDataSource, ProductRepository>();
        //services.AddScoped<IOrderRepository, OrderRepository>();
        //services.AddScoped<ICustomerRepository, CustomerRepository>();
        //services.AddScoped<IPaymentRepository, PaymentRepository>();
        //services.AddScoped<IUserRepository, UserRepository>();
        //services.AddScoped<IPreparationRepository, PreparationRepository>();
        //services.AddScoped<IReadOnlyQuery<GetPreparationMonitorResult>, OrderMonitorQuery>();

        return services;
    }
}
