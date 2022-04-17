namespace Movies.Domain.Entities;

public class Movie : AuditableEntity
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int Release { get; set; }
    public string Duration { get; set; }
    public string MaturityRating { get; set; }
    public string Summary { get; set; }
    public ICollection<Image> Images { get; set; }
    public ICollection<MovieGenre> MovieGenres { get; set; }
    public ICollection<MoviePerson> MoviePersons { get; set; }
}
