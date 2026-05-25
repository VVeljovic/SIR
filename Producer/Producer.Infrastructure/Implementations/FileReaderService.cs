using CsvHelper;
using Producer.Application.Abstractions;
using Producer.Domain.Entities;
using Producer.Infrastructure.Mappings;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Producer.Infrastructure.Implementations
{
    public sealed class FileReaderService : IFileReaderService
    {
        public async IAsyncEnumerable<AccidentRecord> ReadAsync(string filePath, 
            [EnumeratorCancellation]CancellationToken cancellationToken)
        {
            using var reader = new StreamReader(filePath);

            using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
            csvReader.Context.RegisterClassMap<AccidentRecordMap>();

            await foreach (var record in csvReader.GetRecordsAsync<AccidentRecord>(cancellationToken))
            {
                yield return record;
            }
        }
    }
}
