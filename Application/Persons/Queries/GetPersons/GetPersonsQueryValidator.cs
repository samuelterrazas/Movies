namespace Movies.Application.Persons.Queries.GetPersons;

public class GetPersonsQueryValidator : AbstractValidator<GetPersonsQuery>
{
    public GetPersonsQueryValidator()
    {
        RuleFor(p => p.PageNumber)
            .GreaterThanOrEqualTo((short)1);

        RuleFor(p => p.PageSize)
            .GreaterThanOrEqualTo((short)10);

        RuleFor(p => p.Name)
            .MinimumLength(3)
            .MaximumLength(100);
    }
}
