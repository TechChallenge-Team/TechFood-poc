using System.Linq;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TechFood.Application.Common.Services;
using TechFood.Application.Common.Services.Interfaces;

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
        services.AddTransient<IImageUrlResolver, ImageUrlResolver>();

        //MediatR
        services.AddMediatR(typeof(DependecyInjection));

        var mediatR = services.First(s => s.ServiceType == typeof(IMediator));

        services.Replace(ServiceDescriptor.Transient<IMediator, Common.EventualConsistency.Mediator>());
        services.Add(
            new ServiceDescriptor(
                mediatR.ServiceType,
                Common.EventualConsistency.Mediator.ServiceKey,
                mediatR.ImplementationType!,
                mediatR.Lifetime));

        return services;
    }
}
