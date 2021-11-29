using Movies.Domain.Enums;

namespace Movies.Domain.Entities
{
    public class MoviePerson
    {
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public Role Role { get; set; }
        public int Order { get; set; }
    }
}
