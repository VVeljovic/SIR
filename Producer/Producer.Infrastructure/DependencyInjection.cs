using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Producer.Application.Abstractions;
using Producer.Application.Implementations;
using Producer.Infrastructure.BackgorundServices;
using Producer.Infrastructure.Messaging;

namespace Producer.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAccidentStreamService, AccidentStreamService>();

            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(configuration["RabbitMQ:Host"], configuration["RabbitMQ:VirtualHost"], host =>
                    {
                        host.Username(configuration["RabbitMQ:Username"]);
                        host.Password(configuration["RabbitMQ:Password"]);
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });

            services.AddScoped<IMessageSender, MessageSender>();
            services.AddHostedService<ProducerBackgroundService>();
            return services;
        }
    }
}
