using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TechFood.Application.Common.Services.Interfaces;
using TechFood.Domain.Enums;

namespace TechFood.Infra.Services.MercadoPago
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddInfraMercadoPagoPayment(this IServiceCollection services)
        {
            services
                .AddOptions<MercadoPagoOptions>()
                .Configure<IConfiguration>((options, config) =>
                {
                    var configSection = config.GetSection(MercadoPagoOptions.SectionName);
                    configSection.Bind(options);
                });

            services.AddKeyedTransient<IPaymentService, MercadoPagoPaymentService>(PaymentType.QrCode);

            services.AddHttpClient(MercadoPagoOptions.ClientName, (serviceProvider, client) =>
            {
                client.BaseAddress = new Uri(MercadoPagoOptions.BaseAddress);

                client.DefaultRequestHeaders.Authorization = new("Bearer", serviceProvider.GetRequiredService<IOptions<MercadoPagoOptions>>().Value.AccessToken);
            });

            return services;
        }
    }
}
