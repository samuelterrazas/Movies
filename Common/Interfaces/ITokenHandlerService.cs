namespace Movies.Common.Interfaces;

public interface ITokenHandlerService
{
    string GenerateJwtToken(ITokenParameters parameters);
}
