using Movies.Application.Genres.Commands.CreateGenre;
using Movies.Application.Genres.Commands.UpdateGenre;

namespace Movies.Application.IntegrationTests.Genres.Commands;

using static Testing;

public class UpdateGenreTests : TestBase
{
    // NotFoundException: Id was not found.
    [Test]
    public async Task ShouldRequireValidGenreId()
    {
        // Act
        var command = new UpdateGenreCommand(Id: 99, Name: "New genre");
        
        // Assert
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }
    
    // ValidationException: .NotEmpty()
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        // Act
        var command = new CreateGenreCommand(Name: "");
        
        // Assert
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }
    
    // ValidationException: .MaximumLength(50);
    [Test]
    public async Task ShouldRequireMaximumFields()
    {
        // Act
        var command = new CreateGenreCommand(Name: "TEXT TEST TEXT TEST TEXT TEST TEXT TEST TEXT TEST TEXT TEST");
        
        // Assert
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }
    
    // Method
    [Test]
    public async Task ShouldUpdateGenre()
    {
        // Arrange
        var genreId = await SendAsync(new CreateGenreCommand(Name: "New genre"));
        
        // Act
        var command = new UpdateGenreCommand(Id: genreId, Name: "Update genre name");
        await SendAsync(command);

        var genre = await FindAsync<Genre>(genreId);

        // Assert
        genre.Should().NotBeNull();
        genre.Name.Should().Be(command.Name);
        genre.LastModified.Should().NotBeNull();
    }
}