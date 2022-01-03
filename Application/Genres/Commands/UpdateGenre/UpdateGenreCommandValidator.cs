namespace Movies.Application.Genres.Commands.UpdateGenre;

public class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
{
    public UpdateGenreCommandValidator()
    {
        RuleFor(g => g.Id)
            .NotEmpty();
        
        RuleFor(g => g.Name)
            .NotEmpty()
            .MaximumLength(50);
    }
}
