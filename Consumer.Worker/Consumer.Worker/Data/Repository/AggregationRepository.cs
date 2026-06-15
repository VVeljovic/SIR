using Consumer.Worker.Data.Interfaces;
using Producer.Domain.Models;

namespace Consumer.Worker.Data.Repository
{
    public class AggregationRepository(ApplicationDbContext dbContext) : IAggregationRepository
    {
        public void UpsertStatsByDevice(SensorReading reading)
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

        public void UpsertStatsByHour(SensorReading reading)
        {
            var hour = reading.Timestamp.Hour; 

            var existingStats = dbContext.SensorStatsByHour.Where(x => x.Hour == hour).FirstOrDefault();

            if (existingStats is null)
            {
                dbContext.Add(new SensorStatsByHour
                {
                    Id = Guid.NewGuid(),
                    Hour = hour,
                    Count = 1,
                    UpdatedAt = DateTime.UtcNow
                });
            }

            else
            {
                existingStats.Count++;
                existingStats.UpdatedAt = DateTime.UtcNow;
            }
        }
    }
}
