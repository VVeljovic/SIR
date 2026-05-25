using Producer.Domain.Entities;

namespace Producer.Application.Abstractions
{
    public interface IFileReaderService
    {
        IAsyncEnumerable<AccidentRecord> ReadAsync(string filePath, CancellationToken cancellationToken);
    }
}
