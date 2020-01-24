using MediatR;
using Patronage2020.Application.Common.Interfaces;
using Patronage2020.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Patronage2020.Application.System.Commands.SeedSampleData
{
    public class SeedSampleDataCommand : IRequest
    {
    }

    public class SeedSampleDataCommandHandler : IRequestHandler<SeedSampleDataCommand>
    {
        private readonly IPatronage2020DbContext _context;
        private readonly IUserManager _userManager;

        public SeedSampleDataCommandHandler(IPatronage2020DbContext context, IUserManager userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<Unit> Handle(SeedSampleDataCommand request, CancellationToken cancellationToken)
        {
            var seeder = new SampleDataSeeder(_context, _userManager);

            await seeder.SeedAllAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
