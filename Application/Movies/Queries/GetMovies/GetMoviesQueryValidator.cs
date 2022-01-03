namespace Movies.Application.Movies.Queries.GetMovies;

public class GetMoviesQueryValidator : AbstractValidator<GetMoviesQuery>
{
    public GetMoviesQueryValidator()
    {
        RuleFor(m => m.PageNumber)
            .GreaterThanOrEqualTo(1);

        RuleFor(m => m.PageSize)
            .GreaterThanOrEqualTo(10);

        RuleFor(m => m.Title)
            .MinimumLength(3)
            .MaximumLength(50);

        RuleFor(m => m.Genre)
            .MinimumLength(3)
            .MaximumLength(50);

        RuleFor(m => m.Director)
            .MinimumLength(3)
            .MaximumLength(100);

        RuleFor(m => m.Actor)
            .MinimumLength(3)
            .MaximumLength(100);
    }
}
