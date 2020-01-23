using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace Patronage2020.Application.ReversedString.Queries.GetReversedString
{
    public class GetReversedStringQueryValidator : AbstractValidator<GetReversedStringQuery>
    {
        public GetReversedStringQueryValidator()
        {
            RuleFor(x => x.StringToReverse).NotEmpty();
        }
    }
}
