using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Patronage2020.Application.ReversedString.Queries.GetReversedString
{
    public class GetReversedStringQuery : IRequest<ReversedStringDto>
    {
        public string StringToReverse { get; }

        public GetReversedStringQuery(string stringToReverse)
        {
            StringToReverse = stringToReverse;
        }
    }
}
