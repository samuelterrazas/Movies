namespace Movies.Application.Common.Interfaces
{
    public interface ITokenParameters
    {
        string Id { get; set; }
        string Email { get; set; }
        string Role { get; set; }
    }
}
