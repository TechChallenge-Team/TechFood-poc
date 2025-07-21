using Microsoft.Extensions.DependencyInjection;
using TechFood.Application.Interfaces.DataSource;

namespace TechFood.Infrastructure.ImageStore.LocalDisk
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraImageStore(this IServiceCollection services)
        {
            services.AddScoped<IImageDataSource, LocalDiskImageStorageService>();

            return services;
        }
    }
}
