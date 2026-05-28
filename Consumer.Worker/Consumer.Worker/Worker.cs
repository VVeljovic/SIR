using MassTransit;
using Producer.Domain.Entities;
using Serilog; 

namespace Consumer.Worker
{
    public class Worker : IConsumer<AccidentRecord>
    {
        public readonly ILogger<Worker> _logger; 
        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<AccidentRecord> context)
        {
            _logger.LogInformation("Processing accident: {airport}", context.Message.AirportCode);
        }
    }
}
