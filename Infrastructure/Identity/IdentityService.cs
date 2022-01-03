using Microsoft.AspNetCore.Identity;
using Movies.Common.Interfaces;

namespace Movies.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private const string DefaultRole = "User";

    public IdentityService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
        
    public async Task<object> GetEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<bool> CheckUserPasswordAsync(object user, string password)
    {
        var appUser = user as ApplicationUser;
            
        return await _userManager.CheckPasswordAsync(appUser, password);
    }

    public async Task<ITokenParameters> GenerateTokenParametersAsync(object user)
    {
        var appUser = user as ApplicationUser;

        var role = (await _userManager.GetRolesAsync(appUser)).Single();

        return new TokenParameters
        {
            Id = appUser.Id,
            Email = appUser.Email,
            Role = role
        };
    }

    public async Task CreateUserAsync(string email, string password)
    {
        var newUser = new ApplicationUser
        {
            UserName = email,
            Email = email
        };

        await _userManager.CreateAsync(newUser, password);
        await _userManager.AddToRoleAsync(newUser, DefaultRole);
    }
}