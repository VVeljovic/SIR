using MediatR;
using Microsoft.AspNetCore.Mvc;
using Producer.Application.Commands;

namespace Producer.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileController : ControllerBase
    {
        private readonly ISender sender;

        public FileController(ISender sender)
        {
            this.sender = sender;
        }

        [HttpPost("process")]
        public async Task<IActionResult> Process([FromBody] ReadFileCommand request, CancellationToken cancellationToken)
        {
            await sender.Send(request, cancellationToken);

            return Ok();
        }
    }
}
