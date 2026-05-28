using Producer.Application.Abstractions;
using Producer.Infrastructure.Data;

namespace Producer.Infrastructure.Implementations
{
    public sealed class UnitOfWork(ApplicationDbContext dbContext) : IUnitOfWork
    {
        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
