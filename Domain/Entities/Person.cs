using Movies.Domain.Common;

namespace Movies.Domain.Entities;

public class Person : AuditableEntity
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public IEnumerable<MoviePerson> MoviePersons { get; set; }
}