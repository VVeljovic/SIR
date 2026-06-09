using MediatR;
using Microsoft.Extensions.Logging;
using Producer.Application.Abstractions;

namespace Producer.Application.Commands
{
    public sealed class ReadFileCommandHandler(IFileReaderService fileReaderService,
        IMessageSender sender,
        ILogger<ReadFileCommandHandler> logger) : IRequestHandler<ReadFileCommand>
    {
        public async Task Handle(ReadFileCommand request, CancellationToken cancellationToken)
        {

            await foreach (var record in fileReaderService.ReadAsync(request.FilePath, cancellationToken))
            {
                await sender.SendAsync(record, cancellationToken);
                
                logger.LogInformation(
                "Record {Id} | Severity: {Severity} | City: {City} | State: {State}",
                record.Id,
                record.Severity,
                record.City,
                record.State);
            }
        }
    }
}
