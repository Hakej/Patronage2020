using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Patronage2020.Application.ReversedStrings.Queries.GetReversedString;
using Patronage2020.Domain.Entities;

namespace Patronage2020.Application.Common.Mappings
{
    public class ReversedStringProfile : Profile
    {
        public ReversedStringProfile()
        {
            CreateMap<ReversedString, ReversedStringDto>();
        }
    }
}
