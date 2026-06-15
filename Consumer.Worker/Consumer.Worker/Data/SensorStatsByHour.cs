namespace Consumer.Worker.Data
{
    public class SensorStatsByHour
    {
        public Guid Id { get; set; }

        public int Hour { get; set; }

        public int Count { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
