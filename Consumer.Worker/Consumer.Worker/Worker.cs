using Consumer.Worker.Data;
using Producer.Domain.Entities;
using Consumer.Worker.Data.Interfaces;
using MassTransit;

namespace Consumer.Worker
{
    public class Worker : IConsumer<AccidentRecord>
    {
        public readonly ILogger<Worker> _logger;

        public readonly IAccidentByStateRepository _accidentByStateRepository;
        public readonly IUnitOfWork _unitOfWork;

        public Worker(ILogger<Worker> logger, IAccidentByStateRepository accidentByStateRepository, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _accidentByStateRepository = accidentByStateRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<AccidentRecord> context)
        {
            _logger.LogInformation("Processing accident: {state}", context.Message.State);

            _accidentByStateRepository.Upsert(context.Message.State, context.Message.Severity);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
