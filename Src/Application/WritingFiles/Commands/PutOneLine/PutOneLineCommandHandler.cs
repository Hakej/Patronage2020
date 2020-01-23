using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Patronage2020.Application.WritingFiles.Commands.PutOneLine
{
    public class PutOneLineCommandHandler : IRequestHandler<PutOneLineCommand, string>
    {
        // TODO: Simplify the logic
        public Task<string> Handle(PutOneLineCommand request, CancellationToken cancellationToken)
        {
            // TODO: Move directory path to config
            var fileDir = "Data";

            if(!Directory.Exists(fileDir))
            {
                Directory.CreateDirectory(fileDir);
            }

            var fileNumber = 1;
            var lineWasInserted = false;

            while(!lineWasInserted)
            {
                var filePath = Path.Combine(fileDir, $"{fileNumber}.txt");

                if(!File.Exists(filePath + fileNumber))
                {
                    using(var file = new StreamWriter(filePath + fileNumber, true))
                    {
                        file.Write(request.NewLine);
                    }

                    lineWasInserted = true;
                    break;
                }

                var fileInfo = new FileInfo(filePath);
                var fileSize = (int)fileInfo.Length;
                var fileMaxSize = 256;

                if(fileSize >= fileMaxSize)
                {
                    fileNumber++;
                    continue;
                }
                else
                {
                    var newLine = request.NewLine;
                    var totalLength = request.NewLine.Length + fileSize;

                    // If it's too large, we take as many characters as we can and put the rest in the next file
                    if(totalLength > fileMaxSize)
                    {
                        // Measure how many characters will fit in
                        var length = totalLength % fileMaxSize;
                        length = request.NewLine.Length - length;

                        // Get the part that still fits in
                        var firstPart = request.NewLine.Substring(0, length);

                        // Put it in the first file
                        using(var f = new StreamWriter(filePath + fileNumber, true))
                        {
                            f.Write(firstPart);
                        }

                        fileNumber++;

                        // Save the characters we haven't put in yet
                        newLine = request.NewLine.Substring(length);
                    }

                    using(var outputFile = new StreamWriter(filePath + fileNumber, true))
                    {
                        outputFile.Write(newLine);
                    }

                    lineWasInserted = true;
                }
            }

            return Task.FromResult(request.NewLine);
        }
    }
}
