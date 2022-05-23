namespace Movies.Application.IntegrationTests.Movies.Commands;

using static Testing;

public class UpdateMovieTests : TestBase
{
    [Test]
    public async Task ShouldUpdateMovie()
    {
        // Arrange
        var genreIdA = await SendAsync(new CreateGenreCommand(Name: "New name A"));
        var genreIdB = await SendAsync(new CreateGenreCommand(Name: "New name B"));

        var personIdA = await SendAsync(new CreatePersonCommand(FullName: "New fullName A"));
        var personIdB = await SendAsync(new CreatePersonCommand(FullName: "New fullName B"));
        var personIdC = await SendAsync(new CreatePersonCommand(FullName: "New fullName C"));
        
        var createCommand = new CreateMovieCommand(
            Title: "New title",
            Release: 2022,
            Duration: "2h",
            MaturityRating: "18+",
            Summary: "New summary",
            Teaser: "New teaser",
            Genres: new List<int> {genreIdA, genreIdB},
            Persons: new List<MoviePersonDto>
            {
                new(PersonId: personIdA, Role: 1, Order: 1),
                new(PersonId: personIdB, Role: 2, Order: 1),
                new(PersonId: personIdC, Role: 2, Order: 2)
            }
        );

        var movieId = await SendAsync(createCommand);

        // Act
        var updateCommand = new UpdateMovieCommand(
            Id: movieId,
            Title: "Updated title",
            Release: 2021,
            Duration: "2h 40m",
            MaturityRating: "16+",
            Summary: "Updated summary",
            Teaser: "Updated teaser",
            Genres: new List<int> {genreIdA, genreIdB},
            Persons: new List<MoviePersonDto>
            {
                new(PersonId: personIdA, Role: 1, Order: 1),
                new(PersonId: personIdB, Role: 2, Order: 1),
                new(PersonId: personIdC, Role: 2, Order: 2)
            }
        );

        await SendAsync(updateCommand);

        var query = new GetMovieDetailsQuery(Id: movieId);

        var movie = await SendAsync(query);
        
        // Assert
        movie.Should().NotBeNull();
        movie.Title.Should().Be("Updated title");
        movie.Release.Should().Be(2021);
        movie.Duration.Should().Be("2h 40m");
        movie.MaturityRating.Should().Be("16+");
        movie.Summary.Should().Be("Updated summary");
    }
}