using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using Patronage2020.Application.WritingFiles.Models;

namespace Patronage2020.Application.WritingFiles.Commands.PostWritingFile
{
    public class PostWritingFileCommand : IRequest<WritingFileDto>
    {
        public string Content { get; }

        public PostWritingFileCommand(string content)
        {
            Content = content;
        }
    }
}
