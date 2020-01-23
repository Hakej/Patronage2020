using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using Patronage2020.Application.WritingFiles.Models;

namespace Patronage2020.Application.WritingFiles.Queries.GetWritingFile
{
    public class GetWritingFileQuery : IRequest<WritingFileDto>
    {
        public int Id { get; }

        public GetWritingFileQuery()
        {
            Id = 0;
        }

        public GetWritingFileQuery(int id)
        {
            Id = id;
        }
    }
}
