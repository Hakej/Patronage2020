using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Options;
using Patronage2020.Application.Common.Interfaces;
using Patronage2020.Application.WritingFiles.Models;
using Patronage2020.Common;
using Patronage2020.Domain.Entities;

namespace Patronage2020.Application.WritingFiles.Commands.PostWritingFile
{
    public class PostWritingFileCommandHandler : IRequestHandler<PostWritingFileCommand, WritingFile>
    {
        private readonly IOptions<WritingFilesConfig> _config;
        private readonly IPatronage2020DbContext _context;

        public PostWritingFileCommandHandler(IOptions<WritingFilesConfig> config, IPatronage2020DbContext context)
        {
            _config = config;
            _context = context;
        }

        public async Task<WritingFile> Handle(PostWritingFileCommand request, CancellationToken cancellationToken)
        {
            var dirName = _config.Value.DirectoryName;
            var fileNumber = _config.Value.StartingNumber;
            var filePath = Path.Combine(dirName, $"{fileNumber}.txt");

            if(!Directory.Exists(dirName))
            {
                Directory.CreateDirectory(dirName);
            }

            // Delete all existing data
            var dirInfo = new DirectoryInfo(dirName);
            foreach(var file in dirInfo.GetFiles())
            {
                file.Delete();
            }

            using(var file = new StreamWriter(filePath))
            {
                file.Write(request.Content);
            }

            // Update database
            var entity = new WritingFile { Name = $"{fileNumber}.txt", Content = request.Content };
            _context.WritingFiles.RemoveRange(_context.WritingFiles);
            _context.WritingFiles.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return await Task.FromResult(entity);
        }
    }
}
