using Producer.Domain.Models;

namespace Consumer.Worker.Data.Interfaces
{
    public interface IAggregationRepository
    {
        void UpsertStatsByDevice(SensorReading reading);
        void UpsertStatsByHour(SensorReading reading);
        void SaveReading(SensorReading reading);
    }
}
