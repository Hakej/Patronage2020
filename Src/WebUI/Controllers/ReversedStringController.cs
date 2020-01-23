using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Patronage2020.Application.ReversedString.Queries.GetReversedString;
using Patronage2020.Application.ReversedStrings.Queries.GetReversedStringHistory;

namespace Patronage2020.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReversedStringController : BaseController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ReversedStringHistoryDto>> GetReversedStringHistory()
        {
            var query = new GetReversedStringHistoryQuery();
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("stringToReverse")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ReversedStringDto>> GetReversedString(string stringToReverse)
        {
            var query = new GetReversedStringQuery(stringToReverse);
            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}