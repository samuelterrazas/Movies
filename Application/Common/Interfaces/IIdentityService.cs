using System.Threading.Tasks;

namespace Movies.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<object> GetEmailAsync(string email);

        Task<object> GetUserNameAsync(string userName);

        Task<bool> CheckUserPasswordAsync(object user, string password);

        Task<ITokenParameters> GenerateTokenParametersAsync(object user);

        Task CreateUserAsync(string email, string userName, string password);
    }
}