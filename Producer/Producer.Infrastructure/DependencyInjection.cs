using Microsoft.Extensions.DependencyInjection;
using Producer.Application.Abstractions;
using Producer.Infrastructure.Implementations;

namespace Producer.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IFileReaderService, FileReaderService>();
            return services;
        }
    }
}
