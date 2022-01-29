namespace Movies.Application.Images.Commands.UploadImage;

public class UploadImageCommandValidator : AbstractValidator<UploadImageCommand>
{
    public UploadImageCommandValidator()
    {
        RuleFor(i => i.MovieId)
            .NotEmpty();
        
        RuleFor(i => i.Image.Length)
            .LessThanOrEqualTo(10485760).WithMessage("File size is bigger than 10 MB.");
        RuleFor(i => i.Image.ContentType)
            .Must(contentType => contentType.Equals("image/jpeg") || contentType.Equals("image/png"))
                .WithMessage("File extension is not valid.");
    }
}