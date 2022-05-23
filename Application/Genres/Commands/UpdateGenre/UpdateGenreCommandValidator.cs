namespace Movies.Application.Genres.Commands.UpdateGenre;

public class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
{
    public UpdateGenreCommandValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(50);
    }
}
