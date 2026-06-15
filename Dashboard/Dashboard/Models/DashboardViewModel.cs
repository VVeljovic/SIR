using Dashboard.Data;

namespace Dashboard.Models
{
    public class DashboardViewModel
    {
        public List<SensorStatsByDevice> StatsByDevice { get; set; } = [];
        public List<SensorStatsByHour> StatsByHour { get; set; } = [];
        public long TotalMeasurements { get; set; }
        public double AvgTemperature { get; set; }
        public double AvgHumidity { get; set; }
    }
}
