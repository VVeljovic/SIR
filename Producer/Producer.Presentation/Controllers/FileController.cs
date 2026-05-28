using MediatR;
using Microsoft.AspNetCore.Mvc;
using Producer.Application.Commands;

namespace Producer.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileController(ISender sender, IConfiguration configuration) : ControllerBase
    {

        [HttpPost("process")]
        public async Task<IActionResult> Process()
        {
            var command = new ReadFileCommand(configuration["FilePath"]!);
            
            await sender.Send(command);

            return Ok();
        }
    }
}
