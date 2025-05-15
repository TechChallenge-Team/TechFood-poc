using Microsoft.Extensions.DependencyInjection;
using TechFood.Application.Common.Services.Interfaces;

namespace TechFood.Infra.ImageStore.LocalDisk.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraImageStore(this IServiceCollection services)
        {
            services.AddScoped<IImageStorageService, LocalDiskImageStorageService>();

            return services;
        }
    }
}
