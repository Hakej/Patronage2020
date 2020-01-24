using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using Patronage2020.Application.WritingFiles.Models;
using Patronage2020.Domain.Entities;

namespace Patronage2020.Application.WritingFiles.Queries.GetWritingFile
{
    public class GetWritingFileQuery : IRequest<WritingFile>
    {
        public int Id { get; }

        public GetWritingFileQuery(int id)
        {
            Id = id;
        }
    }
}
