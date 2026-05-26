using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Producer.Application.Commands;
using Producer.Application.Data;

namespace Producer.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ReadFileCommandHandler).Assembly));
            services.AddDbContext<ApplicationDbContext>(cfg =>
            {
                cfg.UseNpgsql("Host=localhost;Port=5432;Database=mydb;Username=myuser;Password=mypassword");
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

            return services;
        }
    }
}

