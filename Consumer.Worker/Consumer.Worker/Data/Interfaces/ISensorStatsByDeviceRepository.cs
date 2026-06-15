using Producer.Domain.Models;

namespace Consumer.Worker.Data.Interfaces
{
    public interface ISensorStatsByDeviceRepository
    {
        void Upsert(SensorReading reading);
    }
}
