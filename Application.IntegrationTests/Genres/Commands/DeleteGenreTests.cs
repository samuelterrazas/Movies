namespace Movies.Application.IntegrationTests.Genres.Commands;

using static Testing;

public class DeleteGenreTests : TestBase
{
    [Test]
    public async Task ShouldDeleteGenre()
    {
        // Arrange
        var genreId = await SendAsync(new CreateGenreCommand(Name: "New name"));
        
        // Act
        await SendAsync(new DeleteGenreCommand(Id: genreId));

        var genre = await FindAsync<Genre>(genreId);

        //Assert
        genre.Should().BeNull();
    }
}