namespace Movies.Application.Common.Interfaces
{
    public interface ITokenHandlerService
    {
        string GenerateJwtToken(ITokenParameters parameters);
    }
}
