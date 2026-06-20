using CsvHelper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Producer.Application.Abstractions;
using Producer.Application.Mappings;
using Producer.Application.Settings;
using Producer.Domain.Models;
using System.Globalization;

namespace Producer.Application.Implementations
{
    public sealed class AccidentStreamService(IMessageSender messageSender,
        ILogger<AccidentStreamService> logger,
        IOptionsMonitor<AppSettings> optionsMonitor) : IAccidentStreamService
    {
        public async Task StreamAsync(CancellationToken cancellationToken)
        {
            var filePath = optionsMonitor.CurrentValue.FilePath;

            using var reader = new StreamReader(filePath);

            using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
            csvReader.Context.RegisterClassMap<SensorReadingMap>();

            await foreach (var record in csvReader.GetRecordsAsync<SensorReading>(cancellationToken))
            {
                var delay = optionsMonitor.CurrentValue.StreamDelay;
                logger.LogInformation("Delay is {Delay}", delay);

                logger.LogInformation(
                    "SensorReading {timestamp} | Humidity {Severity} | {Device} | {Co} sent to queue",
                    record.Timestamp,
                    record.Humidity,
                    record.Device,
                    record.Co);

                await messageSender.SendAsync(record, cancellationToken);

                await Task.Delay(delay, cancellationToken);
            }
        }
    }
}
