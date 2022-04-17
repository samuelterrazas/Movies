namespace Movies.Domain.Entities;

public class Image : AuditableEntity
{
    public int Id { get; set; }
    public int MovieId { get; set; }
    public Movie Movie { get; set; }
    public string Url { get; set; }
}