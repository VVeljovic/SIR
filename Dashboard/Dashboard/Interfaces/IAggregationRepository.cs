using Dashboard.Data;

namespace Dashboard.Interfaces
{
    public interface IAggregationRepository
    {
        Task<List<SensorStatsByDevice>> GetSensorStatsByDeviceAsync();
        Task<List<SensorStatsByHour>> GetSensorStatsByHoursAsync();
        Task<List<SensorReading>> GetAllReadingsAsync();
    }
}
