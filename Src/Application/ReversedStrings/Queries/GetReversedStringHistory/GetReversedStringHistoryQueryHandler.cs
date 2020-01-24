using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Options;
using Patronage2020.Domain.Entities;
using Patronage2020.WebUI.Common;
using Serilog;

namespace Patronage2020.Application.ReversedStrings.Queries.GetReversedStringHistory
{
    public class GetReversedStringHistoryQueryHandler : IRequestHandler<GetReversedStringHistoryQuery, ReversedStringHistory>
    {
        private readonly IOptions<LoggingConfig> _config;

        public GetReversedStringHistoryQueryHandler(IOptions<LoggingConfig> config)
        {
            _config = config;
        }

        public Task<ReversedStringHistory> Handle(GetReversedStringHistoryQuery request, CancellationToken cancellationToken)
        {
            var filePath = Path.Combine(_config.Value.DirectoryName, _config.Value.FileName);
            string[] history;

            // Workaround for Serilog's bug that makes logging file unaccessible 
            using(FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using(var sr = new StreamReader(fs, Encoding.UTF8))
                {
                    var content = sr.ReadToEnd();
                    content = content.Trim();
                    history = content.Split("\r\n");
                }
            }

            return Task.FromResult(new ReversedStringHistory { History = new List<string>(history) });
        }
    }
}
