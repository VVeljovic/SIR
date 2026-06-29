using Dashboard.Data;

namespace Dashboard.Interfaces
{
    public interface IAggregationRepository
    {
        public List<SensorStatsByDevice> GetSensorStatsByDevice();
        public List<SensorStatsByHour> GetSensorStatsByHours();
        public List<SensorReading> GetAllReadings();
    }
}
