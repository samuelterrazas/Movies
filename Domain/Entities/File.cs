using Movies.Domain.Common;

namespace Movies.Domain.Entities;

public class File : AuditableEntity
{
    public int Id { get; set; }
    public int MovieId { get; set; }
    public Movie Movie { get; set; }
    public string Url { get; set; }
}