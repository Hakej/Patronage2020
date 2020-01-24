using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Options;
using Patronage2020.Application.Common.Exceptions;
using Patronage2020.Application.Common.Interfaces;
using Patronage2020.Application.WritingFiles.Models;
using Patronage2020.Common;
using Patronage2020.Domain.Entities;

namespace Patronage2020.Application.WritingFiles.Queries.GetWritingFile
{
    public class GetWritingFileQueryHandler : IRequestHandler<GetWritingFileQuery, WritingFile>
    {
        private readonly IOptions<WritingFilesConfig> _config;
        private readonly IPatronage2020DbContext _context;

        public GetWritingFileQueryHandler(IOptions<WritingFilesConfig> config, IPatronage2020DbContext context)
        {
            _config = config;
            _context = context;
        }

        public async Task<WritingFile> Handle(GetWritingFileQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.WritingFiles.FindAsync(request.Id);

            if (entity == null)
            {
                throw new WritingFileNotFoundException(request.Id);
            }

            var builder = new StringBuilder();
            var dirName = _config.Value.DirectoryName;
            var filePath = Path.Combine(dirName, entity.Name);
            builder.Append(File.ReadAllText(filePath));

            return await Task.FromResult(new WritingFile { Id = entity.Id, Name = entity.Name, Content = builder.ToString() });
        }
    }
}
