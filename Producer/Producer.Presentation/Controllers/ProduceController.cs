using MediatR;
using Microsoft.AspNetCore.Mvc;
using Producer.Application.Commands;

namespace Producer.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProduceController(ISender sender, IConfiguration configuration) : ControllerBase
    {
        [HttpPost("produce")]
        public async Task<IActionResult> Produce(CancellationToken cancellationToken)
        {
            var command = new ReadFileCommand(configuration["FilePath"]!);

            await sender.Send(command, cancellationToken);

            return Ok();
        }
    }
}
