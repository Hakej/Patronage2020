using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace Patronage2020.Application.WritingFiles.Commands.PostWritingFile
{
    public class PostWritingFileCommandValidator : AbstractValidator<PostWritingFileCommand>
    {
        public PostWritingFileCommandValidator()
        {
            RuleFor(x => x.Content).MaximumLength(50);
        }
    }
}
