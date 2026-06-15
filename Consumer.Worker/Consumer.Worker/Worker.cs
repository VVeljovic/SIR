using Consumer.Worker.Data.Interfaces;
using MassTransit;
using Producer.Domain.Models;

namespace Consumer.Worker
{
    public class Worker : IConsumer<SensorReading>
    {
        public readonly ILogger<Worker> _logger;

        public readonly IUnitOfWork _unitOfWork;

        public readonly IAggregationRepository _aggregationRepository;

        public Worker(ILogger<Worker> logger,
            IAggregationRepository aggregationRepository,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _aggregationRepository = aggregationRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<SensorReading> context)
        {
            _aggregationRepository.UpsertStatsByDevice(context.Message);
            _aggregationRepository.UpsertStatsByHour(context.Message);

            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("Processing reading: {sensor}", context.Message.Device);
        }
    }
}
