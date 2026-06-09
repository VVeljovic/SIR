using Producer.Domain.Entities;

namespace Producer.Application.Abstractions
{
    public interface IAccidentStreamService
    {
        Task StreamAsync(CancellationToken cancellationToken);
    }
}
