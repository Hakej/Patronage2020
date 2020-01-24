using Microsoft.EntityFrameworkCore;
using Patronage2020.Application.Common.Interfaces;
using Patronage2020.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
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
        private readonly Dictionary<int, WritingFile> WritingFiles = new Dictionary<int, WritingFile>();

        public SampleDataSeeder(IPatronage2020DbContext context, IUserManager userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task SeedAllAsync(CancellationToken cancellationToken)
        {
            if(_context.WritingFiles.Any())
            {
                return;
            }

            await SeedWritingFilesAsync(cancellationToken);
        }

        private async Task SeedWritingFilesAsync(CancellationToken cancellationToken)
        {
            var writingFiles = new[]
            {
                new WritingFile { Id = 1, Name = "1.txt" }
            };

            using(var stream = File.Create(Path.Combine("Data", "1.txt")))
                
            _context.WritingFiles.AddRange(writingFiles);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}