using Dashboard.Data;
using Dashboard.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dashboard.Implementations
{
    public class AggregationRepository(ApplicationDbContext dbContext) : IAggregationRepository
    {
        public Task<List<SensorStatsByDevice>> GetSensorStatsByDeviceAsync()
            => dbContext.SensorStatsByDevice.ToListAsync();

        public Task<List<SensorStatsByHour>> GetSensorStatsByHoursAsync()
            => dbContext.SensorStatsByHour.ToListAsync();

        public Task<List<SensorReading>> GetAllReadingsAsync()
            => dbContext.SensorReading
                .OrderByDescending(x => x.Timestamp)
                .Take(3000)
                .OrderBy(x => x.Timestamp)
                .ToListAsync();
    }
}
