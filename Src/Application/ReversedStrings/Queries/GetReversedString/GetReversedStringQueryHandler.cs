using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Patronage2020.Domain.Entities;
using Serilog;

namespace Patronage2020.Application.ReversedStrings.Queries.GetReversedString
{
    public class GetReversedStringQueryHandler : IRequestHandler<GetReversedStringQuery, ReversedString>
    {
        public Task<ReversedString> Handle(GetReversedStringQuery request, CancellationToken cancellationToken)
        {
            var arrayToReverse = request.StringToReverse.ToCharArray();
            Array.Reverse(arrayToReverse);
            var reversedString = new string(arrayToReverse);

            Log.Information(($"{request.StringToReverse} -> {reversedString}"));

            return Task.FromResult(new ReversedString { Content = reversedString });
        }
    }
}
