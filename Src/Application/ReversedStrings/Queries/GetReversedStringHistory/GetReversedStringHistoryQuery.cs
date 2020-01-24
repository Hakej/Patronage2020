using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using Patronage2020.Domain.Entities;

namespace Patronage2020.Application.ReversedStrings.Queries.GetReversedStringHistory
{
    public class GetReversedStringHistoryQuery : IRequest<ReversedStringHistory>
    {
    }
}
