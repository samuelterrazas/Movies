namespace Movies.Application.Files.Commands.UploadFile;

public class UploadFileCommandValidator : AbstractValidator<UploadFileCommand>
{
    public UploadFileCommandValidator()
    {
        RuleFor(file => file.MovieId)
            .NotEmpty();
        
        RuleFor(file => file.Image.Length)
            .LessThanOrEqualTo(10485760).WithMessage("File size is bigger than 10 MB.");
        RuleFor(file => file.Image.ContentType)
            .Must(contentType => contentType.Equals("image/jpeg") || contentType.Equals("image/png"))
                .WithMessage("File extension is not valid.");
    }
}