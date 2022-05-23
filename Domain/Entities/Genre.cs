namespace Movies.Domain.Entities;

public class Genre : AuditableEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<MovieGenre>? MovieGenres { get; set; }
}
