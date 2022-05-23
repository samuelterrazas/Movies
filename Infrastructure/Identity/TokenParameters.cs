namespace Movies.Infrastructure.Identity;

public class TokenParameters : ITokenParameters
{
    public string Id { get; init; } = null!;
    public string Email { get; init; } = null!;
    public string Role { get; init; } = null!;
}
