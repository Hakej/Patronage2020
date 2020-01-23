using Microsoft.EntityFrameworkCore;

namespace Patronage2020.Persistence
{
    public class Patronage2020DbContextFactory : DesignTimeDbContextFactoryBase<Patronage2020DbContext>
    {
        protected override Patronage2020DbContext CreateNewInstance(DbContextOptions<Patronage2020DbContext> options)
        {
            return new Patronage2020DbContext(options);
        }
    }
}
