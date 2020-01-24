using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Patronage2020.Application.Common.Interfaces;

namespace Patronage2020.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<Patronage2020DbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Patronage2020Database")));

            services.AddScoped<IPatronage2020DbContext>(provider => provider.GetService<Patronage2020DbContext>());

            return services;
        }
    }
}
