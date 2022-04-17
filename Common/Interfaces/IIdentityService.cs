namespace Movies.Common.Interfaces;

public interface IIdentityService
{
    Task<object> GetEmailAsync(string email);

    Task<bool> CheckPasswordAsync(object user, string password);

    Task<Result> CreateAsync(string email, string password);

    Task<Result> AddToRoleAsync(string email, string role);
    
    Task<ITokenParameters> GenerateTokenParametersAsync(object user);
}