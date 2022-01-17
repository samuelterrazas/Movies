namespace Movies.Infrastructure.Identity;

public record Jwt(string Secret, int AccessTokenExpiration);
