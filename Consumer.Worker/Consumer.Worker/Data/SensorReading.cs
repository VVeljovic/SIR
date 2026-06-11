namespace Producer.Domain.Models
{
    public class SensorReading
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime Timestamp { get; set; }
        public string Device { get; set; }
        public double Co { get; set; }
        public double Humidity { get; set; }
        public bool Light { get; set; }
        public double Lpg { get; set; }
        public bool Motion { get; set; }
        public double Smoke { get; set; }
        public double Temperature { get; set; }
    }
}
