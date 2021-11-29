using FluentValidation;

namespace Movies.Application.Genres.Queries.GetGenres
{
    public class GetGenresQueryValidator : AbstractValidator<GetGenresQuery>
    {
        public GetGenresQueryValidator()
        {
            RuleFor(g => g.PageNumber)
                .GreaterThanOrEqualTo(1);

            RuleFor(g => g.PageSize)
                .GreaterThanOrEqualTo(1);
        }
    }
}
