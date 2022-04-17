namespace Movies.Infrastructure.Identity;

public class TokenParameters : ITokenParameters
{
    public string Id { get; init; }
    public string Email { get; init; }
    public string Role { get; init; }
}
