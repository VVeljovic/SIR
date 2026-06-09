using CsvHelper;
using Microsoft.Extensions.Logging;
using Producer.Application.Abstractions;
using Producer.Application.Mappings;
using Producer.Domain.Entities;
using System.Globalization;

namespace Producer.Application.Implementations
{
    public sealed class AccidentStreamService(IMessageSender messageSender, ILogger<AccidentStreamService> logger) : IAccidentStreamService
    {
        public async Task StreamAsync(CancellationToken cancellationToken)
        {
            using var reader = new StreamReader("C:\\Users\\Veljko\\Downloads\\US_Accidents_March23.csv");

            using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
            csvReader.Context.RegisterClassMap<AccidentRecordMap>();

            await foreach (var record in csvReader.GetRecordsAsync<AccidentRecord>(cancellationToken))
            {
                logger.LogInformation(
                    "Accident {AccidentId} | Severity {Severity} | {State} | {City} sent to queue",
                    record.Id,
                    record.Severity,
                    record.State,
                    record.City);

                await messageSender.SendAsync(record, cancellationToken);
            }
        }
    }
}
