using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Patronage2020.Application.ReversedStrings.Queries.GetReversedString;
using Patronage2020.Application.ReversedStrings.Queries.GetReversedStringHistory;
using Patronage2020.Domain.Entities;

namespace Patronage2020.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReversedStringController : BaseController
    {
        private readonly IMapper _mapper;

        public ReversedStringController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ReversedStringHistoryDto>> GetReversedStringHistory()
        {
            var query = new GetReversedStringHistoryQuery();
            var result = await Mediator.Send(query);
            var resultDto = _mapper.Map<ReversedStringHistoryDto>(result);
            return Ok(resultDto);
        }

        [HttpGet("stringToReverse")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ReversedString>> GetReversedString(string stringToReverse)
        {
            var query = new GetReversedStringQuery(stringToReverse);
            var result = await Mediator.Send(query);
            var resultDto = _mapper.Map<ReversedStringDto>(result);
            return Ok(resultDto);
        }
    }
}