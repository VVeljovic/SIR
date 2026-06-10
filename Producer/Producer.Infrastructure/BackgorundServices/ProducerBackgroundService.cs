using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Producer.Application.Abstractions;

namespace Producer.Infrastructure.BackgorundServices
{
    public sealed class ProducerBackgroundService(IServiceScopeFactory scopeFactory,
        ILogger<ProducerBackgroundService> logger) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("Streaming started...");

            var scope = scopeFactory.CreateScope();
            
            var accidentStreamService = scope.ServiceProvider.GetRequiredService<IAccidentStreamService>();

            await accidentStreamService.StreamAsync(stoppingToken);
        }
    }
}
