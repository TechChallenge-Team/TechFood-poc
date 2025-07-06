using Microsoft.Extensions.DependencyInjection;
using TechFoodClean.Application.Interfaces.DataSource;

namespace TechFoodClean.Infrastructure.ImageStore.LocalDisk
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
