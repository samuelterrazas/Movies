namespace Movies.Application.Images.Commands.UpdateImage;

public class UpdateImageCommandValidator : AbstractValidator<UpdateImageCommand>
{
    public UpdateImageCommandValidator()
    {
        RuleFor(p => p.MovieId)
            .NotEmpty();
        
        RuleFor(p => p.Image.Length)
            .LessThanOrEqualTo(10485760).WithMessage("File size is bigger than 10 MB.");
        RuleFor(p => p.Image.ContentType)
            .Must(contentType => contentType.Equals("image/jpeg") || contentType.Equals("image/png"))
            .WithMessage("File extension is not valid.");
    }
}