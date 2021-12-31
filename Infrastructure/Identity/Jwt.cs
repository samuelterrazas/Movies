namespace Movies.Infrastructure.Identity;

public class Jwt
{
    public string Secret { get; set; }
    public int AccessTokenExpiration { get; set; }
}
