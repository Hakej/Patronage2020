using System;
using AutoMapper;
using Patronage2020.Application.Common.Mappings;
using Patronage2020.Persistence;
using Xunit;

namespace Patronage2020.Application.UnitTests.Common
{
    public class QueryTestFixture : IDisposable
    {
        public Patronage2020DbContext Context { get; private set; }
        public IMapper Mapper { get; private set; }

        public QueryTestFixture()
        {
            Context = Patronage2020DbContextFactory.Create();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            Mapper = configurationProvider.CreateMapper();
        }

        public void Dispose()
        {
            Patronage2020DbContextFactory.Destroy(Context);
        }
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}