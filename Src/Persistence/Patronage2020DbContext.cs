using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Patronage2020.Application.Common.Interfaces;
using Patronage2020.Common;
using Patronage2020.Domain.Entities;
using Patronage2020.Domain.Common;

namespace Patronage2020.Persistence
{
    public class Patronage2020DbContext : DbContext, IPatronage2020DbContext
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;

        public Patronage2020DbContext(DbContextOptions<Patronage2020DbContext> options)
            : base(options)
        {
        }

        public Patronage2020DbContext(
            DbContextOptions<Patronage2020DbContext> options, 
            ICurrentUserService currentUserService,
            IDateTime dateTime)
            : base(options)
        {
            _currentUserService = currentUserService;
            _dateTime = dateTime;
        }

        public DbSet<WritingFile> WritingFiles { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUserService.UserId;
                        entry.Entity.Created = _dateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _currentUserService.UserId;
                        entry.Entity.LastModified = _dateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Patronage2020DbContext).Assembly);
        }
    }
}
