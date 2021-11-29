using Movies.Application.Common.Mappings;
using Movies.Domain.Entities;

namespace Movies.Application.Movies.DTOs
{
    public class MovieDto : IMapFrom<Movie>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
    }
}
