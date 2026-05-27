using Producer.Domain.Entities;

namespace Producer.Application.Abstractions
{
    public interface IMessageSender
    {
        Task SendAsync(AccidentRecord record, CancellationToken cancellationToken);
    }
}
