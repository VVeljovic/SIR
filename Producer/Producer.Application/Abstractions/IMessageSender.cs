using Producer.Domain.Models;

namespace Producer.Application.Abstractions
{
    public interface IMessageSender
    {
        Task SendAsync(SensorReading record, CancellationToken cancellationToken);
    }
}
