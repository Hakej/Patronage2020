using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Patronage2020.Application.WritingFiles.Commands;
using Patronage2020.Application.WritingFiles.Commands.PostWritingFile;
using Patronage2020.Application.WritingFiles.Models;
using Patronage2020.Application.WritingFiles.Queries.GetWritingFile;

namespace Patronage2020.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WritingFileController : BaseController
    {
        [HttpGet("id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<WritingFileDto>> GetWritingFile(int id = 0)
        {
            var query = new GetWritingFileQuery(id);
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("content")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<WritingFileDto>> PostWritingFile(string content)
        {
            var command = new PostWritingFileCommand(content);
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("line")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<WritingFileDto>> PutOneLine(string newLine)
        {
            var command = new PutOneLineCommand(newLine);
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}