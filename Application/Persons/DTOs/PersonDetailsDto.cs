using AutoMapper;
using Movies.Application.Common.Mappings;
using Movies.Application.Movies.DTOs;
using Movies.Domain.Entities;

namespace Movies.Application.Persons.DTOs;

public class PersonDetailsDto : IMapFrom<Person>
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public List<MovieDto> Movies { get; set; }

        
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Person, PersonDetailsDto>()
            .ForMember(personDto => personDto.Movies, options => options.MapFrom(MoviesDto));
    }

    private List<MovieDto> MoviesDto(Person person, PersonDetailsDto personDto) =>
        (from moviePerson in person.MoviePersons
            select new MovieDto
            {
                Id = moviePerson.MovieId, 
                Title = moviePerson.Movie.Title, 
                Image = moviePerson.Movie.Image
            }
        ).ToList();
}