using FluentValidation;

namespace Movies.Application.Genres.Commands.CreateGenre
{
    public class CreateGenreCommandValidator : AbstractValidator<CreateGenreCommand>
    {
        public CreateGenreCommandValidator()
        {
            RuleFor(g => g.Name)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}
