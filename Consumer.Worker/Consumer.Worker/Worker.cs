using Consumer.Worker.Data.Interfaces;
using MassTransit;
using Producer.Domain.Models;

namespace Consumer.Worker
{
    public class Worker : IConsumer<SensorReading>
    {
        public readonly ILogger<Worker> _logger;

        public readonly IUnitOfWork _unitOfWork;

        public Worker(ILogger<Worker> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<SensorReading> context)
        {
            _logger.LogInformation("Processing reading: {sensor}", context.Message.Device);
        }
    }
}
