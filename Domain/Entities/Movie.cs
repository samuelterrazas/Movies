namespace Movies.Domain.Entities;

public class Movie : AuditableEntity
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public short Release { get; set; }
    public string Duration { get; set; } = null!;
    public string MaturityRating { get; set; } = null!;
    public string Summary { get; set; } = null!;
    public string Teaser { get; set; } = null!;
    public ICollection<Image>? Images { get; set; }
    public ICollection<MovieGenre>? MovieGenres { get; set; }
    public ICollection<MoviePerson>? MoviePersons { get; set; }
}
