using FluentValidation;

namespace Movies.Application.Movies.Commands.UpdateMovie
{
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
                .MaximumLength(30);

            RuleFor(m => m.MaturityRating)
                .NotEmpty()
                .MaximumLength(10);

            RuleFor(m => m.Summary)
                .NotEmpty()
                .MaximumLength(1200);

            RuleFor(m => m.Image.Length)
                .LessThanOrEqualTo(10485760).WithMessage("File size is bigger than 10 MB.");
            RuleFor(m => m.Image.ContentType)
                .Must(i => i.Equals("image/jpeg") || i.Equals("image/png")).WithMessage("File extension is not valid.");

            RuleFor(m => m.Genres)
                .NotEmpty();

            RuleFor(m => m.Persons)
                .NotEmpty()
                .ForEach(i => i.ChildRules(j => j.RuleFor(k => k.Role)
                    .LessThanOrEqualTo(2)
                    .GreaterThanOrEqualTo(1)));
        }
    }
}
