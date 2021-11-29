using AutoMapper;
using Movies.Application.Common.Mappings;
using Movies.Application.Genres.DTOs;
using Movies.Domain.Entities;
using Movies.Domain.Enums;
using System.Collections.Generic;
using System.Linq;
using Movies.Application.Persons.DTOs;

namespace Movies.Application.Movies.DTOs
{
    public class MovieDetailsDto : IMapFrom<Movie>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Release { get; set; }
        public string Duration { get; set; }
        public string MaturityRating { get; set; }
        public string Summary { get; set; }
        public string Image { get; set; }
        public List<GenreDto> Genres { get; set; }
        public List<PersonDto> DirectedBy { get; set; }
        public List<PersonDto> Cast { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Movie, MovieDetailsDto>()
                .ForMember(movieDto => movieDto.Genres, options => options.MapFrom(GenresDto))
                .ForMember(movieDto => movieDto.DirectedBy, options => options.MapFrom(DirectorsDto))
                .ForMember(movieDto => movieDto.Cast, options => options.MapFrom(CastDto));
        }

        private IList<GenreDto> GenresDto(Movie movie, MovieDetailsDto movieDto) =>
            (from movieGenre in movie.MovieGenres 
                select new GenreDto
                {
                    Id = movieGenre.GenreId, 
                    Name = movieGenre.Genre.Name
                }
            ).ToList();

        private IList<PersonDto> DirectorsDto(Movie movie, MovieDetailsDto movieDto) =>
            (from moviePerson in movie.MoviePersons 
                where moviePerson.Role == Role.Director 
                orderby moviePerson.Order
                select new PersonDto
                {
                    Id = moviePerson.PersonId, 
                    FullName = moviePerson.Person.FullName
                }
            ).ToList();

        private IList<PersonDto> CastDto(Movie movie, MovieDetailsDto movieDto) =>
            (from moviePerson in movie.MoviePersons
                where moviePerson.Role == Role.Cast
                orderby moviePerson.Order
                select new PersonDto
                {
                    Id = moviePerson.PersonId, 
                    FullName = moviePerson.Person.FullName
                }
            ).ToList();
    }
}
