using Microsoft.AspNetCore.Identity;
using Patronage2020.Application.Common.Models;
using System.Linq;

namespace Patronage2020.Infrastructure.Identity
{
    public static class IdentityResultExtensions
    {
        public static Result ToApplicationResult(this IdentityResult result)
        {
            return result.Succeeded
                ? Result.Success()
                : Result.Failure(result.Errors.Select(e => e.Description));
        }
    }
}