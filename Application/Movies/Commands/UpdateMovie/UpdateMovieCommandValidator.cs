namespace Movies.Application.Movies.Commands.UpdateMovie;

public class UpdateMovieCommandValidator : AbstractValidator<UpdateMovieCommand>
{
    public UpdateMovieCommandValidator()
    {
        RuleFor(m => m.Title)
            .NotEmpty()
            .MaximumLength(60);

        RuleFor(m => m.Release)
            .NotEmpty()
            .LessThanOrEqualTo(32767)
            .GreaterThanOrEqualTo(1895);

        RuleFor(m => m.Duration)
            .NotEmpty()
            .MaximumLength(10);

        RuleFor(m => m.MaturityRating)
            .NotEmpty()
            .MaximumLength(10);

        RuleFor(m => m.Summary)
            .NotEmpty()
            .MaximumLength(1200);

        RuleFor(m => m.Genres)
            .NotEmpty();

        RuleFor(m => m.Persons)
            .NotEmpty()
            .ForEach(initialCollection => 
                initialCollection.ChildRules(inlineValidator => 
                    inlineValidator.RuleFor(moviePersonDto => moviePersonDto.Role)
                        .LessThanOrEqualTo(2)
                        .GreaterThanOrEqualTo(1)
                )
            );
    }
}