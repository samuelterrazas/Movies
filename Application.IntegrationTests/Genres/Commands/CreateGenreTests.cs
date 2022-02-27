using Movies.Application.Genres.Commands.CreateGenre;
using Movies.Application.Genres.Queries.GetGenres;

namespace Movies.Application.IntegrationTests.Genres.Commands;

using static Testing;

public class CreateGenreTests : TestBase
{
    [Test]
    public async Task ShouldCreateGenre()
    {
        // Act
        var command = new CreateGenreCommand(Name: "New name");

        var genreId = await SendAsync(command);

        var genre = await FindAsync<Genre>(genreId);

        // Assert
        genre.Should().NotBeNull();
        genre.Name.Should().Be("New name");
        genre.Created.Should().NotBeNull();
        genre.LastModified.Should().BeNull();
    }
}