namespace Movies.Domain.Entities;

public class Person : AuditableEntity
{
    public int Id { get; set; }
    public string FullName { get; set; } = null!;
    public ICollection<MoviePerson>? MoviePersons { get; set; }
}