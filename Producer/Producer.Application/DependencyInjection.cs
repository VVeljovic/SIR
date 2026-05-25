using Microsoft.Extensions.DependencyInjection;
using Producer.Application.Commands;

namespace Producer.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ReadFileCommandHandler).Assembly));
            return services;
        }
    }
}
