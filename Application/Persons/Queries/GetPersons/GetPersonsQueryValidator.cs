namespace Movies.Application.Persons.Queries.GetPersons;

public class GetPersonsQueryValidator : AbstractValidator<GetPersonsQuery>
{
    public GetPersonsQueryValidator()
    {
        RuleFor(p => p.PageNumber)
            .GreaterThanOrEqualTo(1);

        RuleFor(p => p.PageSize)
            .GreaterThanOrEqualTo(10);

        RuleFor(p => p.Name)
            .MinimumLength(3)
            .MaximumLength(100);
    }
}
