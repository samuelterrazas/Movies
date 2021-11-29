namespace Movies.Application.Common.Models
{
    public class Result
    {
        private Result(bool succeeded, string token)
        {
            Succeeded = succeeded;
            Token = token;
        }

        public bool Succeeded { get; }
        public string Token { get; }

        public static Result Success() => new(true, string.Empty);

        public static Result LoggedIn(string token) => new(true, token);
    }
}