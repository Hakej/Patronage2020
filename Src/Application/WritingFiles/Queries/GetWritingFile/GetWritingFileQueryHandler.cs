using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Patronage2020.Application.Common.Exceptions;
using Patronage2020.Application.WritingFiles.Models;

namespace Patronage2020.Application.WritingFiles.Queries.GetWritingFile
{
    public class GetWritingFileQueryHandler : IRequestHandler<GetWritingFileQuery, WritingFileDto>
    {
        public Task<WritingFileDto> Handle(GetWritingFileQuery request, CancellationToken cancellationToken)
        {            
            // TODO: Move directory path to config
            var fileDir = "Data";
            var builder = new StringBuilder();

            if(!Directory.Exists(fileDir))
            {
                Directory.CreateDirectory(fileDir);
            }

            // Read all files if id == 0
            if(request.Id == 0)
            {
                var dirInfo = new DirectoryInfo(fileDir);

                foreach(var fileInfo in dirInfo.GetFiles())
                {
                    using(var file = new StreamReader(fileInfo.FullName))
                    {
                        builder.Append(file.ReadToEnd());
                    }
                }
            }
            else
            {
                try
                {
                    var filePath = Path.Combine(fileDir, $"{request.Id}.txt");
                    builder.Append(File.ReadAllText(filePath));
                }
                catch(Exception ex)
                {
                    throw new WritingFileNotFoundException(request.Id, ex);
                }
            }

            return Task.FromResult(new WritingFileDto { Id = request.Id, Content = builder.ToString() });
        }
    }
}
