using Consumer.Worker.Data.Interfaces;
using MassTransit;
using Producer.Domain.Models;

namespace Consumer.Worker
{
    public class Worker : IConsumer<SensorReading>
    {
        public readonly ILogger<Worker> _logger;

        public readonly IUnitOfWork _unitOfWork;

        public readonly ISensorStatsByDeviceRepository _sensorStatsByDeviceRepository;

        public Worker(ILogger<Worker> logger,
            ISensorStatsByDeviceRepository sensorStatsByDeviceRepository,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _sensorStatsByDeviceRepository = sensorStatsByDeviceRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<SensorReading> context)
        {
            _sensorStatsByDeviceRepository.Upsert(context.Message);
            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation("Processing reading: {sensor}", context.Message.Device);
        }
    }
}
