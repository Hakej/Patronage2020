using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Patronage2020.Domain.Entities;

namespace Patronage2020.Application.Common.Interfaces
{
    public interface IPatronage2020DbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
