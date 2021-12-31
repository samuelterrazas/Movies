using FluentValidation;

namespace Movies.Application.Movies.Queries.GetMovies;

public class GetMoviesQueryValidator : AbstractValidator<GetMoviesQuery>
{
    public GetMoviesQueryValidator()
    {
        RuleFor(m => m.PageNumber)
            .GreaterThanOrEqualTo(1);

        RuleFor(m => m.PageSize)
            .GreaterThanOrEqualTo(1);

        RuleFor(m => m.Title)
            .MaximumLength(50);

        RuleFor(m => m.Genre)
            .MaximumLength(50);

        RuleFor(m => m.Director)
            .MaximumLength(100);

        RuleFor(m => m.Actor)
            .MaximumLength(100);
    }
}
