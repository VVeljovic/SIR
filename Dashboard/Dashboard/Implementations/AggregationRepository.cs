using Dashboard.Data;
using Dashboard.Interfaces;

namespace Dashboard.Implementations
{
    public class AggregationRepository(ApplicationDbContext dbContext) : IAggregationRepository
    {
        public List<SensorStatsByDevice> GetSensorStatsByDevice()
        {
            return dbContext.SensorStatsByDevice.ToList();
        }

        public List<SensorStatsByHour> GetSensorStatsByHours()
        {
            return dbContext.SensorStatsByHour.ToList();
        }

        public List<SensorReading> GetAllReadings()
        {
            return dbContext.SensorReading.OrderBy(x => x.Timestamp).ToList();
        }
    }
}
