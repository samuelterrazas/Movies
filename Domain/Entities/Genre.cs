using Movies.Domain.Common;

namespace Movies.Domain.Entities;

public class Genre : AuditableEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<MovieGenre> MovieGenres { get; set; }
}
