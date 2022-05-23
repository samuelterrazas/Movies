namespace Movies.Application.Genres.Commands.CreateGenre;

public class CreateGenreCommandValidator : AbstractValidator<CreateGenreCommand>
{
    public CreateGenreCommandValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(50);
    }
}
