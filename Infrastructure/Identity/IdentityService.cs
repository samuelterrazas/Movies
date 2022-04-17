namespace Movies.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public IdentityService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
        
    public async Task<object> GetEmailAsync(string email) => await _userManager.FindByEmailAsync(email);

    public async Task<bool> CheckPasswordAsync(object user, string password)
    {
        var appUser = (ApplicationUser)user;
            
        return await _userManager.CheckPasswordAsync(appUser, password);
    }

    public async Task<Result> CreateAsync(string email, string password)
    {
        var newUser = new ApplicationUser {UserName = email, Email = email};

        var result = await _userManager.CreateAsync(newUser, password);

        return result.ToApplicationResult();
    }

    public async Task<Result> AddToRoleAsync(string email, string role)
    {
        var user = await _userManager.FindByEmailAsync(email);

        var result = await _userManager.AddToRoleAsync(user, role);

        return result.ToApplicationResult();
    }
    
    public async Task<ITokenParameters> GenerateTokenParametersAsync(object user)
    {
        var appUser = (ApplicationUser)user;

        var role = (await _userManager.GetRolesAsync(appUser)).First();

        return new TokenParameters {Id = appUser?.Id, Email = appUser?.Email, Role = role};
    }
}