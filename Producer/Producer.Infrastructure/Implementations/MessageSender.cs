using MassTransit;
using Producer.Application.Abstractions;
using Producer.Domain.Entities;

namespace Producer.Infrastructure.Implementations
{
    public sealed class MessageSender(ISendEndpointProvider sendEndpointProvider) : IMessageSender
    {
        public async Task SendAsync(AccidentRecord record, CancellationToken cancellationToken)
        {
            var endpoint = await sendEndpointProvider.GetSendEndpoint(new Uri("queue:testqueue"));

            await endpoint.Send(record);
        }
    }
}
