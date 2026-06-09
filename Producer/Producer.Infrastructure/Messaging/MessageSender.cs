using MassTransit;
using Producer.Application.Abstractions;
using Producer.Domain.Entities;

namespace Producer.Infrastructure.Messaging
{
    public sealed class MessageSender(ISendEndpointProvider sendEndpointProvider) : IMessageSender
    {
        public async Task SendAsync(AccidentRecord record, CancellationToken cancellationToken)
        {
            var endpoint = await sendEndpointProvider.GetSendEndpoint(new Uri("queue:nov"));

            await endpoint.Send(record, cancellationToken);
        }
    }
}
