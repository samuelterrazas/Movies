using Movies.Common.Interfaces;

namespace Movies.Infrastructure.Identity;

public class TokenParameters : ITokenParameters
{
    public string Id { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
}
