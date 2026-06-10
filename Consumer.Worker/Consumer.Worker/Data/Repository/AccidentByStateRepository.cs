using Consumer.Worker.Data.Interfaces;

namespace Consumer.Worker.Data.Repository
{
    public class AccidentByStateRepository(ApplicationDbContext dbContext) : IAccidentByStateRepository
    {
        public void Upsert(string state, int severity)
        {
            var accident = dbContext.AccidentsByState
                .Where(x => x.State == state)
                .FirstOrDefault();

            if (accident is null)
            {
                dbContext.AccidentsByState.Add(new AccidentByState
                {
                    Id = Guid.NewGuid(),
                    State = state,
                    Count = 1,
                    AvgSeverity = severity,
                    UpdatedAt = DateTime.UtcNow,
                });
            }

            else
            {
                accident.Count++;
                accident.AvgSeverity = (accident.AvgSeverity * (accident.Count - 1) + severity)
                                       / accident.Count;
                accident.UpdatedAt = DateTime.UtcNow;
            }
        }
    }
}
