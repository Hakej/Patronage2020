using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Patronage2020.Application.WritingFiles.Models;
using Patronage2020.Domain.Entities;

namespace Patronage2020.Application.Common.Mappings
{
    public class WritingFileProfile : Profile
    {
        public WritingFileProfile()
        {
            CreateMap<WritingFile, WritingFileDto>();
        }
    }
}
