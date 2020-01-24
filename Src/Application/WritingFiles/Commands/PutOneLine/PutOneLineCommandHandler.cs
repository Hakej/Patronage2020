using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Options;
using Patronage2020.Application.Common.Interfaces;
using Patronage2020.Common;
using Patronage2020.Domain.Entities;

namespace Patronage2020.Application.WritingFiles.Commands.PutOneLine
{
    public class PutOneLineCommandHandler : IRequestHandler<PutOneLineCommand, string>
    {
        private readonly IOptions<WritingFilesConfig> _config;
        private readonly IPatronage2020DbContext _context;

        public PutOneLineCommandHandler(IOptions<WritingFilesConfig> config, IPatronage2020DbContext context)
        {
            _config = config;
            _context = context;
        }

        public async Task<string> Handle(PutOneLineCommand request, CancellationToken cancellationToken)
        {
            // Get newest file
            var writingFile = _context.WritingFiles
                .OrderByDescending(p => p.Id)
                .FirstOrDefault();

            var dirName = _config.Value.DirectoryName;
            var fileMaxSize = _config.Value.MaxFileSizeInBytes;
            var filePath = Path.Combine(dirName, writingFile.Name);
            var fileInfo = new FileInfo(filePath);
            var fileSize = (int)fileInfo.Length;

            if(!Directory.Exists(dirName))
            {
                Directory.CreateDirectory(dirName);
            }

            if(fileSize == fileMaxSize)
            {
                var newId = writingFile.Id + 1;
                filePath = Path.Combine(dirName, $"{newId}.txt");
                using(var newFile = new StreamWriter(filePath))
                {
                    newFile.Write(request.NewLine);
                }

                return await Task.FromResult(request.NewLine);
            }

            var totalLength = request.NewLine.Length + fileSize;

            if(totalLength <= fileMaxSize)
            {
                using(var outputFile = new StreamWriter(filePath, true))
                {
                    outputFile.Write(request.NewLine);
                }

                return await Task.FromResult(request.NewLine);
            }

            var excessLength = totalLength % fileMaxSize;
            excessLength = request.NewLine.Length - excessLength;
            var firstPart = request.NewLine.Substring(0, excessLength);
            var secondPart = request.NewLine.Substring(excessLength);
            var secondId = writingFile.Id + 1;
            var secondFileName = $"{secondId}.txt";
            var secondFilePath = Path.Combine(dirName, secondFileName);

            using(var firstFile = new StreamWriter(filePath, true))
            {
                firstFile.Write(firstPart);
            }            

            using(var secondFile = new StreamWriter(secondFilePath))
            {
                secondFile.Write(secondPart);
            }

            _context.WritingFiles.Add(new WritingFile { Name = secondFileName, Content = secondPart });
            await _context.SaveChangesAsync(cancellationToken);

            return await Task.FromResult(request.NewLine);
        }
    }
}
