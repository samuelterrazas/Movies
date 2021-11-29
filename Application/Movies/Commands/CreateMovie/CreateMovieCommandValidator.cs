using FluentValidation;

namespace Movies.Application.Movies.Commands.CreateMovie
{
    public class CreateMovieCommandValidator : AbstractValidator<CreateMovieCommand>
    {
        public CreateMovieCommandValidator()
        {
            RuleFor(m => m.Title)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(m => m.Release)
                .NotEmpty()
                .LessThanOrEqualTo(32767)
                .GreaterThanOrEqualTo(1895);

            RuleFor(m => m.Duration)
                .NotEmpty()
                .MaximumLength(25);

            RuleFor(m => m.MaturityRating)
                .NotEmpty()
                .MaximumLength(10);

            RuleFor(m => m.Summary)
                .NotEmpty()
                .MaximumLength(1500);

            RuleFor(m => m.Image.Length)
                .LessThanOrEqualTo(10485760).WithMessage("File size is bigger than 10 MB.");
            RuleFor(m => m.Image.ContentType)
                .Must(m => m.Equals("image/jpeg") || m.Equals("image/png")).WithMessage("File extension is not valid.");

            RuleFor(m => m.Genres)
                .NotEmpty();

            RuleFor(m => m.Persons)
                .NotEmpty()
                .ForEach(m => m.ChildRules(i => i.RuleFor(j => j.Role)
                    .LessThanOrEqualTo(2)
                    .GreaterThanOrEqualTo(1)));
        }
    }
}
