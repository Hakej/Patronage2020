using Microsoft.EntityFrameworkCore;
using Patronage2020.Application.Common.Interfaces;
using Patronage2020.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Patronage2020.Persistence
{
    public class SampleDataSeeder
    {
        private readonly IPatronage2020DbContext _context;
        private readonly IUserManager _userManager;

        // Data to seed
        // private readonly Dictionary<int, Song> Songs = new Dictionary<int, Song>();

        public SampleDataSeeder(IPatronage2020DbContext context, IUserManager userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task SeedAllAsync(CancellationToken cancellationToken)
        {

        }
    }
}