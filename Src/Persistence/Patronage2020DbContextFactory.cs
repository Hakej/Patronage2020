using System;
using Microsoft.EntityFrameworkCore;
using Patronage2020.Domain.Entities;

namespace Patronage2020.Persistence
{
    public class Patronage2020DbContextFactory : DesignTimeDbContextFactoryBase<Patronage2020DbContext>
    {
        protected override Patronage2020DbContext CreateNewInstance(DbContextOptions<Patronage2020DbContext> options)
        {
            return new Patronage2020DbContext(options);
        }

        public static Patronage2020DbContext Create()
        {
            var options = new DbContextOptionsBuilder<Patronage2020DbContext>().Options;

            var context = new Patronage2020DbContext(options);

            context.Database.EnsureCreated();

            context.WritingFiles.AddRange(new[] {
                new WritingFile { Id = 1, Name = "1.txt", Content = "First"},
                new WritingFile { Id = 2, Name = "2.txt", Content = "Second"},
                new WritingFile { Id = 3, Name = "3.txt", Content = "Third"},
            });

            context.SaveChanges();

            return context;
        }

        public static void Destroy(Patronage2020DbContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}
