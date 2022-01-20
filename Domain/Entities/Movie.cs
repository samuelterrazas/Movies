using Movies.Domain.Common;

namespace Movies.Domain.Entities;

public class Movie : AuditableEntity
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int Release { get; set; }
    public string Duration { get; set; }
    public string MaturityRating { get; set; }
    public string Summary { get; set; }
    public IEnumerable<File> Files { get; set; }
    public IEnumerable<MovieGenre> MovieGenres { get; set; }
    public IEnumerable<MoviePerson> MoviePersons { get; set; }
}
