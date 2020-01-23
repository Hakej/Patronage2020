using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Patronage2020.Application.WritingFiles.Models;

namespace Patronage2020.Application.WritingFiles.Commands.PostWritingFile
{
    public class PostWritingFileCommandHandler : IRequestHandler<PostWritingFileCommand, WritingFileDto>
    {
        public Task<WritingFileDto> Handle(PostWritingFileCommand request, CancellationToken cancellationToken)
        {
            // TODO: Move directory path to config
            var fileDir = "Data";
            var filePath = Path.Combine(fileDir, "1.txt");

            if(!Directory.Exists(fileDir))
            {
                Directory.CreateDirectory(fileDir);
            }

            // Delete all existing data
            var dirInfo = new DirectoryInfo(fileDir);
            foreach(var file in dirInfo.GetFiles())
            {
                file.Delete();
            }

            using(var file = new StreamWriter(filePath))
            {
                file.Write(request.Content);
            }

            return Task.FromResult(new WritingFileDto { Id = 1, Content = request.Content });
        }
    }
}
