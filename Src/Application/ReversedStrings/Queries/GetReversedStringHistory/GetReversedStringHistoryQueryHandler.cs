using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Options;

namespace Patronage2020.Application.ReversedStrings.Queries.GetReversedStringHistory
{
    public class GetReversedStringHistoryQueryHandler : IRequestHandler<GetReversedStringHistoryQuery, ReversedStringHistoryDto>
    {

        public Task<ReversedStringHistoryDto> Handle(GetReversedStringHistoryQuery request, CancellationToken cancellationToken)
        {
            // TODO: Move directory path to config
            var history = File.ReadAllText("Logs/reversed-strings-history.txt");
            return Task.FromResult(new ReversedStringHistoryDto { History = new List<string>{ history } });
        }
    }
}
