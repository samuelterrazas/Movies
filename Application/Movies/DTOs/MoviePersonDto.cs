using Movies.Application.Common.Mappings;
using Movies.Domain.Entities;

namespace Movies.Application.Movies.DTOs
{
    public class MoviePersonDto : IMapFrom<Person>
    {
        public int PersonId { get; set; }
        public int Role { get; set; }
        public int Order { get; set; }
    }
}
