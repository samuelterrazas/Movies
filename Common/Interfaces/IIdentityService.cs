namespace Movies.Common.Interfaces;

public interface IIdentityService
{
    Task<object> GetEmailAsync(string email);

    Task<bool> CheckUserPasswordAsync(object user, string password);

    Task<ITokenParameters> GenerateTokenParametersAsync(object user);

    Task CreateUserAsync(string email, string password);
}