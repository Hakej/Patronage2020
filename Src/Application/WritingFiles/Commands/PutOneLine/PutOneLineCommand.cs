using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Patronage2020.Application.WritingFiles.Commands
{
    public class PutOneLineCommand : IRequest<string>
    {
        public string NewLine { get; }

        public PutOneLineCommand(string newLine)
        {
            NewLine = Environment.NewLine + newLine;
        }
    }
}
