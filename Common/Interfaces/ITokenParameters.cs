namespace Movies.Common.Interfaces;

public interface ITokenParameters
{
    string Id { get; init; }
    string Email { get; init; }
    string Role { get; init; }
}
