using Microsoft.Extensions.DependencyInjection;
using TechFood.Application.Common.Services;
using TechFood.Application.Common.Services.Interfaces;
using TechFood.Application.UseCases;
using TechFood.Application.UseCases.Interfaces;

namespace TechFood.Application;

public static class DependecyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        //Services
        services.AddTransient<IEmailSender, EmailSender>();

        //AutoMapper
        services.AddAutoMapper(typeof(DependecyInjection));

        services.AddSingleton<IOrderNumberService, OrderNumberService>();

        services.AddTransient<ICategoryUseCase, CategoryUseCase>();
        services.AddTransient<IProductUseCase, ProductUseCase>();
        services.AddTransient<IOrderUseCase, OrderUseCase>();
        services.AddTransient<IPaymentUseCase, PaymentUseCase>();
        services.AddTransient<ICustomerUseCase, CustomerUseCase>();
        services.AddTransient<IMenuUseCase, MenuUseCase>();
        services.AddTransient<IAuthUseCase, AuthUseCase>();
        services.AddTransient<IPreparationUseCase, PreparationUseCase>();

        services.AddTransient<ICustomerUseCase, CustomerUseCase>();
        services.AddTransient<IImageUrlResolver, ImageUrlResolver>();

        return services;
    }
}
