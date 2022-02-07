using Movies.Application.Genres.Commands.CreateGenre;
using Movies.Application.Genres.Commands.DeleteGenre;

namespace Movies.Application.IntegrationTests.Genres.Commands;

using static Testing;

public class DeleteGenreTests : TestBase
{
    // NotFoundException: Id was not found.
    [Test]
    public async Task ShouldRequireValidGenreId()
    {
        // Act
        var command = new DeleteGenreCommand(Id: 99);
        
        // Assert
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }
    
    // Method
    [Test]
    public async Task ShouldDeleteGenre()
    {
        // Arrange
        var genreId = await SendAsync(new CreateGenreCommand(Name: "New genre"));
        
        // Act
        await SendAsync(new DeleteGenreCommand(Id: genreId));

        var genre = await FindAsync<Genre>(genreId);

        //Assert
        genre.Should().BeNull();
    }
}