using Consumer.Worker.Data.Interfaces;

namespace Consumer.Worker.Data.Repository
{
    public class UnitOfWork(ApplicationDbContext dbContext) : IUnitOfWork
    {
        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
