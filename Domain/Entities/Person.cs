using Movies.Domain.Common;

namespace Movies.Domain.Entities;

public class Person : AuditableEntity
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public ICollection<MoviePerson> MoviePersons { get; set; }
}