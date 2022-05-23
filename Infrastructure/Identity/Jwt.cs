namespace Movies.Infrastructure.Identity;

public class Jwt
{
    public string Secret { get; init; } = null!;
    public int AccessTokenExpiration { get; init; }
}
