using Consumer.Worker.Data.Interfaces;
using Producer.Domain.Models;

namespace Consumer.Worker.Data.Repository
{
    public class SensorStatsByDeviceRepository(ApplicationDbContext dbContext) : ISensorStatsByDeviceRepository
    {
        public void Upsert(SensorReading reading)
        {
            var existing = dbContext.SensorStatsByDevice
                .Where(x => x.Device == reading.Device)
                .FirstOrDefault();

            if (existing is null)
            {
                dbContext.SensorStatsByDevice.Add(new SensorStatsByDevice
                {
                    Id = Guid.NewGuid(),
                    Device = reading.Device,
                    Count = 1,
                    AvgTemperature = reading.Temperature,
                    AvgHumidity = reading.Humidity,
                    AvgCo = reading.Co,
                    AvgSmoke = reading.Smoke,
                    AvgLpg = reading.Lpg,
                    UpdatedAt = DateTime.UtcNow
                });
            }
            else
            {
                existing.Count++;
                existing.AvgCo = UpdateAvg(existing.AvgCo, existing.Count, reading.Co);
                existing.AvgHumidity = UpdateAvg(existing.AvgHumidity, existing.Count, reading.Humidity);
                existing.AvgLpg = UpdateAvg(existing.AvgLpg, existing.Count, reading.Lpg);
                existing.AvgSmoke = UpdateAvg(existing.AvgSmoke, existing.Count, reading.Smoke);
                existing.AvgTemperature = UpdateAvg(existing.AvgTemperature, existing.Count, reading.Temperature);
            }
        }

        private static double UpdateAvg(double currentAvg, int newCount, double newValue)
        => (currentAvg * (newCount - 1) + newValue) / newCount;
    }
}
