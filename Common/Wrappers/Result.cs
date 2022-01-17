namespace Movies.Common.Wrappers;

public class Result
{
    private Result(bool succeeded, IEnumerable<string> errors,string token)
    {
        Succeeded = succeeded;
        Errors = errors.ToArray();
        Token = token;
    }

    public bool Succeeded { get; }
    public string[] Errors { get; }
    public string Token { get; }

    public static Result Success() => new(true, Array.Empty<string>(), string.Empty);

    public static Result Failure(IEnumerable<string> errors) => new(false, errors, string.Empty);

    public static Result LoggedIn(string token) => new(true, Array.Empty<string>(), token);
}