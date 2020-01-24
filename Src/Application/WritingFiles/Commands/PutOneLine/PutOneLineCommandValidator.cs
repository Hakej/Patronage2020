using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace Patronage2020.Application.WritingFiles.Commands.PutOneLine
{
    public class PutOneLineCommandValidator : AbstractValidator<PutOneLineCommand>
    {
        public PutOneLineCommandValidator()
        {
            RuleFor(x => x.NewLine).MaximumLength(50);
        }
    }
}
