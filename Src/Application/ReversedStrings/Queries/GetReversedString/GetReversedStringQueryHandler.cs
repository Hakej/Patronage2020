using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Patronage2020.Application.ReversedString.Queries.GetReversedString
{
    public class GetReversedStringQueryHandler : IRequestHandler<GetReversedStringQuery, ReversedStringDto>
    {
        public Task<ReversedStringDto> Handle(GetReversedStringQuery request, CancellationToken cancellationToken)
        {
            var arrayToReverse = request.StringToReverse.ToCharArray();
            Array.Reverse(arrayToReverse);
            var reversedString = new string(arrayToReverse);

            Log.Information(($"{request.StringToReverse} -> {reversedString}"));

            return Task.FromResult(new ReversedStringDto { ReversedString = reversedString });
        }
    }
}
