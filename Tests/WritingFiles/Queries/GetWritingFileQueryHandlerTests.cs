using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Patronage2020.Application.UnitTests.Common;
using Patronage2020.Application.WritingFiles.Queries.GetWritingFile;
using Patronage2020.Persistence;
using Xunit;

namespace Patronage2020.UnitTests.WritingFiles.Queries
{
    [Collection("QueryCollection")]
    public class GetWritingFileQueryHandlerTests
    {
        private readonly Patronage2020DbContext _context;
        private readonly IMapper _mapper;

        public GetWritingFileQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetWritingFile()
        {
        }
    }
}