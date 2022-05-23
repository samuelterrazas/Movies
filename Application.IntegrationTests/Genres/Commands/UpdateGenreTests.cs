namespace Movies.Application.IntegrationTests.Genres.Commands;

using static Testing;

public class UpdateGenreTests : TestBase
{
    [Test]
    public async Task ShouldUpdateGenre()
    {
        // Arrange
        var genreId = await SendAsync(new CreateGenreCommand(Name: "New name"));
        
        // Act
        var command = new UpdateGenreCommand(Id: genreId, Name: "Updated name");
        await SendAsync(command);

        var genre = await FindAsync<Genre>(genreId);

        // Assert
        genre.Should().NotBeNull();
        genre!.Name.Should().Be(command.Name);
        genre.LastModified.Should().NotBeNull();
    }
}