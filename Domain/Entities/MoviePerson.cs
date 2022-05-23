namespace Movies.Domain.Entities;

public class MoviePerson
{
    public int MovieId { get; set; }
    public Movie Movie { get; set; } = null!;
    public int PersonId { get; set; }
    public Person Person { get; set; } = null!;
    public byte Role { get; set; }
    public byte Order { get; set; }
}