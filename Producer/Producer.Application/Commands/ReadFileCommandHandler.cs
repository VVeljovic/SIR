using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Producer.Application.Abstractions;
using Producer.Domain.Entities;

namespace Producer.Application.Commands
{
    public sealed class ReadFileCommandHandler(IFileReaderService fileReaderService,
        ISendEndpointProvider sendEndpointProvider,
        ILogger<ReadFileCommandHandler> logger) : IRequestHandler<ReadFileCommand>
    {
        public async Task Handle(ReadFileCommand request, CancellationToken cancellationToken)
        {

            await foreach (var record in fileReaderService.ReadAsync(request.FilePath, cancellationToken))
            {
                var endpoint = await sendEndpointProvider.GetSendEndpoint(new Uri("queue:testqueue"));

                await endpoint.Send(record);
                
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
