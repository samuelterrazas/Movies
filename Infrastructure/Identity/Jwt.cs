namespace Movies.Infrastructure.Identity;

public class Jwt
{
    public string Secret { get; init; }
    public int AccessTokenExpiration { get; init; }
}
