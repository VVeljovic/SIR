using CsvHelper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Producer.Application.Abstractions;
using Producer.Application.Mappings;
using Producer.Domain.Models;
using System.Globalization;

namespace Producer.Application.Implementations
{
    public sealed class AccidentStreamService(IMessageSender messageSender,
        ILogger<AccidentStreamService> logger,
        IConfiguration configuration) : IAccidentStreamService
    {
        public async Task StreamAsync(CancellationToken cancellationToken)
        {
            var filePath = configuration["FilePath"]!;
            var streamDelay = Int32.Parse(configuration["StreamDelay"]!);

            using var reader = new StreamReader(filePath);

            using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
            csvReader.Context.RegisterClassMap<SensorReadingMap>();

            await foreach (var record in csvReader.GetRecordsAsync<SensorReading>(cancellationToken))
            {
                logger.LogInformation(
                    "SensorReading {timestamp} | Humidity {Severity} | {Device} | {Co} sent to queue",
                    record.Timestamp,
                    record.Humidity,
                    record.Device,
                    record.Co);

                await messageSender.SendAsync(record, cancellationToken);

                await Task.Delay(streamDelay);
            }
        }
    }
}
