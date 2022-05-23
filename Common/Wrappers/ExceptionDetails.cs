namespace Movies.Common.Wrappers;

public record ExceptionDetails(string Reference, string Title, int? StatusCode, object Message);