namespace Consumer.Worker.Data
{
    public class AccidentByState
    {
        public Guid Id { get; set; }

        public string State { get; set; }

        public int Count { get; set; }

        public double AvgSeverity { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
