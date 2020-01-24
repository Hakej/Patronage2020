using System;
using AutoMapper;
using Patronage2020.Application.ReversedStrings.Queries.GetReversedString;
using Patronage2020.Application.ReversedStrings.Queries.GetReversedStringHistory;
using Patronage2020.Application.WritingFiles.Models;
using Patronage2020.Domain.Entities;
using Shouldly;
using Xunit;

namespace UnitTests
{
    public class MappingTests : IClassFixture<MappingTestsFixture>
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public MappingTests(MappingTestsFixture fixture)
        {
            _configuration = fixture.ConfigurationProvider;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public void ShouldHaveValidConfiguration()
        {
            _configuration.AssertConfigurationIsValid();
        }

        [Fact]
        public void ShouldMapWritingFileToWritingFileDto()
        {
            var entity = new WritingFile();

            var result = _mapper.Map<WritingFileDto>(entity);

            result.ShouldNotBeNull();
            result.ShouldBeOfType<WritingFileDto>();
        }

        [Fact]
        public void ShouldMapReversedStringToReversedStringDto()
        {
            var entity = new ReversedString();

            var result = _mapper.Map<ReversedStringDto>(entity);

            result.ShouldNotBeNull();
            result.ShouldBeOfType<ReversedStringDto>();
        }

        [Fact]
        public void ShouldMapReversedStringHistoryToReversedStringHistoryDto()
        {
            var entity = new ReversedStringHistory();

            var result = _mapper.Map<ReversedStringHistoryDto>(entity);

            result.ShouldNotBeNull();
            result.ShouldBeOfType<ReversedStringHistoryDto>();
        }
    }
}
