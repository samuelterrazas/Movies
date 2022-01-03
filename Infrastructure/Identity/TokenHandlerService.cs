using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Movies.Common.Interfaces;

namespace Movies.Infrastructure.Identity;

public class TokenHandlerService : ITokenHandlerService
{
    private readonly Jwt _jwt;

    public TokenHandlerService(IOptionsMonitor<Jwt> optionsMonitor)
    {
        _jwt = optionsMonitor.CurrentValue;
    }
        
    public string GenerateJwtToken(ITokenParameters parameters)
    {
        var jwtTokenHandler = new JwtSecurityTokenHandler();

        var key = Encoding.ASCII.GetBytes(_jwt.Secret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, parameters.Id),
                new Claim(ClaimTypes.Email, parameters.Email),
                new Claim(ClaimTypes.Role, parameters.Role)
            }),
            Expires = DateTime.Now.AddMinutes(_jwt.AccessTokenExpiration),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = jwtTokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = jwtTokenHandler.WriteToken(token);

        return jwtToken;
    }
}
