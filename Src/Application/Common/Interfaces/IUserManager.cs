using Patronage2020.Application.Common.Models;
using System.Threading.Tasks;

namespace Patronage2020.Application.Common.Interfaces
{
    public interface IUserManager
    {
        Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password);

        Task<Result> DeleteUserAsync(string userId);
    }
}
