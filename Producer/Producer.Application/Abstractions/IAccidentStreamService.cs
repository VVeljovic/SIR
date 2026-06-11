namespace Producer.Application.Abstractions
{
    public interface IAccidentStreamService
    {
        Task StreamAsync(CancellationToken cancellationToken);
    }
}
