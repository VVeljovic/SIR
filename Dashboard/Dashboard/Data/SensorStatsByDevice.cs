namespace Dashboard.Data
{
    public class SensorStatsByDevice
    {
        public Guid Id { get; set; }
        public string Device { get; set; }
        public double AvgCo { get; set; }
        public double AvgHumidity { get; set; }
        public double AvgLpg { get; set; }
        public double AvgSmoke { get; set; }
        public double AvgTemperature { get; set; }
        public int Count { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
