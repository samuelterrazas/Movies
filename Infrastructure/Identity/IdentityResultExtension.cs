using Microsoft.AspNetCore.Identity;
using Movies.Common.Wrappers;

namespace Movies.Infrastructure.Identity;

public static class IdentityResultExtension
{
    public static Result ToApplicationResult(this IdentityResult result)
        => result.Succeeded
            ? Result.Success()
            : Result.Failure(result.Errors.Select(e => e.Description));
}