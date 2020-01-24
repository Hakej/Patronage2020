using System;
using Patronage2020.Persistence;

namespace Patronage2020.Application.UnitTests.Common
{
    public class CommandTestBase : IDisposable
    {
        protected readonly Patronage2020DbContext _context;

        public CommandTestBase()
        {
            // _context = Patronage2020DbContextFactory.Create();
        }

        public void Dispose()
        {
            // NorthwindContextFactory.Destroy(_context);
        }
    }
}