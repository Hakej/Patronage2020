using AutoMapper;
using Patronage2020.Application.ReversedStrings.Queries.GetReversedStringHistory;
using Patronage2020.Domain.Entities;

namespace Patronage2020.Application.Common.Mappings
{
    public class ReversedStringHistoryProfile : Profile
    {
        public ReversedStringHistoryProfile()
        {
            CreateMap<ReversedStringHistory, ReversedStringHistoryDto>();
        }
    }
}
