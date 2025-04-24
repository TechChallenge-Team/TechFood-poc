using Microsoft.Extensions.DependencyInjection;
using TechFood.Application.Common.Services;
using TechFood.Application.Common.Services.Interfaces;
using TechFood.Application.UseCases;
using TechFood.Application.UseCases.Interfaces;

namespace TechFood.Application
{
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
            services.AddTransient<IOrderUseCase, OrderUseCase>();

            return services;
        }
    }
}
