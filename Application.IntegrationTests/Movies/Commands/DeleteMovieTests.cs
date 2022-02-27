using Movies.Application.Genres.Commands.CreateGenre;
using Movies.Application.Movies.Commands.CreateMovie;
using Movies.Application.Movies.Commands.DeleteMovie;
using Movies.Application.Persons.Commands.CreatePerson;
using Movies.Common.DTOs;

namespace Movies.Application.IntegrationTests.Movies.Commands;

using static Testing;

public class DeleteMovieTests : TestBase
{
    [Test]
    public async Task ShouldDeleteMovie()
    {
        // Arrange
        var genreIdA = await SendAsync(new CreateGenreCommand(Name: "New name A"));
        var genreIdB = await SendAsync(new CreateGenreCommand(Name: "New name B"));

        var personIdA = await SendAsync(new CreatePersonCommand(FullName: "New fullName A"));
        var personIdB = await SendAsync(new CreatePersonCommand(FullName: "New fullName B"));
        var personIdC = await SendAsync(new CreatePersonCommand(FullName: "New fullName C"));
        
        var command = new CreateMovieCommand
        (
            Title: "New title",
            Release: 2022,
            Duration: "2h",
            MaturityRating: "18+",
            Summary: "New summary",
            Genres: new List<int> {genreIdA, genreIdB},
            Persons: new List<MoviePersonDto>
            {
                new(PersonId: personIdA, Role: 1, Order: 1),
                new(PersonId: personIdB, Role: 2, Order: 1),
                new(PersonId: personIdC, Role: 2, Order: 2)
            }
        );

        var movieId = await SendAsync(command);
        
        // Act
        await SendAsync(new DeleteMovieCommand(Id: movieId));

        var movie = await FindAsync<Movie>(movieId);

        // Assert
        movie.Should().BeNull();
    }
}