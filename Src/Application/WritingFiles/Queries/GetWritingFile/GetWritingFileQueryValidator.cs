using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace Patronage2020.Application.WritingFiles.Queries.GetWritingFile
{
    public class GetWritingFileQueryValidator : AbstractValidator<GetWritingFileQuery>
    {
        public GetWritingFileQueryValidator()
        {
            RuleFor(x => x.Id).GreaterThanOrEqualTo(0);
        }
    }
}
