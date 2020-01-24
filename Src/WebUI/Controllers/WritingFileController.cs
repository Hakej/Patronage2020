using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public WritingFileController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet("id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<WritingFileDto>> GetWritingFile(int id)
        {
            var query = new GetWritingFileQuery(id);
            var result = await Mediator.Send(query);
            var resultDto = _mapper.Map<WritingFileDto>(result);
            return Ok(resultDto);
        }

        [HttpPost("content")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<WritingFileDto>> PostWritingFile(string content)
        {
            var command = new PostWritingFileCommand(content);
            var result = await Mediator.Send(command);
            var resultDto = _mapper.Map<WritingFileDto>(result);
            return Ok(resultDto);
        }

        [HttpPut("line")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> PutOneLine(string newLine)
        {
            var command = new PutOneLineCommand(newLine);
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}