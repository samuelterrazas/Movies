namespace Movies.Common.Wrappers;

public class ExceptionDetails
{
    public string Reference { get; init; }
    public string Title { get; init; }
    public int? StatusCode { get; init; }
    public object Message { get; init; }
}