using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using Patronage2020.Domain.Entities;

namespace Patronage2020.Application.ReversedStrings.Queries.GetReversedString
{
    public class GetReversedStringQuery : IRequest<ReversedString>
    {
        public string StringToReverse { get; }

        public GetReversedStringQuery(string stringToReverse)
        {
            StringToReverse = stringToReverse;
        }
    }
}
