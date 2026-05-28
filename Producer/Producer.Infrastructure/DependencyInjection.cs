using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Producer.Application.Abstractions;
using Producer.Infrastructure.Data;
using Producer.Infrastructure.Implementations;

namespace Producer.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IFileReaderService, FileReaderService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddDbContext<ApplicationDbContext>(cfg =>
            {
                cfg.UseNpgsql(configuration.GetConnectionString("Database"));
            }
            );

            services.AddMassTransit(x =>
            {
                x.AddEntityFrameworkOutbox<ApplicationDbContext>(cfg =>
                {
                    cfg.QueryDelay = TimeSpan.FromSeconds(1);   
                    cfg.UsePostgres().UseBusOutbox();

                });

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

            return services;
        }
    }
}
