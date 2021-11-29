using Movies.Application.Common.Mappings;
using Movies.Domain.Entities;

namespace Movies.Application.Genres.DTOs
{
    public class GenreDto : IMapFrom<Genre>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
