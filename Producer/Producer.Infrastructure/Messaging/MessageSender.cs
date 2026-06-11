using MassTransit;
using Producer.Application.Abstractions;
using Producer.Domain.Models;

namespace Producer.Infrastructure.Messaging
{
    public sealed class MessageSender(ISendEndpointProvider sendEndpointProvider) : IMessageSender
    {
        public async Task SendAsync(SensorReading record, CancellationToken cancellationToken)
        {
            var endpoint = await sendEndpointProvider.GetSendEndpoint(new Uri("queue:sensor-data"));

            await endpoint.Send(record, cancellationToken);
        }
    }
}
