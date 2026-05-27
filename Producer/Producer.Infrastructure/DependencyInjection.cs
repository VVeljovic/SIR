using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Producer.Application.Abstractions;
using Producer.Infrastructure.Data;
using Producer.Infrastructure.Implementations;

namespace Producer.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IFileReaderService, FileReaderService>();

            services.AddDbContext<ApplicationDbContext>(cfg =>
            {
                cfg.UseNpgsql("Host=localhost;Port=5433;Database=mydb;Username=postgres;Password=postgres");
            }
            );

            services.AddMassTransit(x =>
            {
                x.AddEntityFrameworkOutbox<ApplicationDbContext>(cfg =>
                {
                    cfg.UsePostgres();

                    cfg.UseBusOutbox();
                });

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", "/", host =>
                    {
                        host.Username("user");
                        host.Password("password");
                    });

                });
            });

            services.AddScoped<IMessageSender, MessageSender>();

            return services;
        }
    }
}
