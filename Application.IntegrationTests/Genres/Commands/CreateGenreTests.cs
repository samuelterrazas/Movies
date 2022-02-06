using FluentAssertions;
using Movies.Application.Genres.Commands.CreateGenre;
using Movies.Common.Exceptions;
using Movies.Domain.Entities;
using NUnit.Framework;

namespace Movies.Application.IntegrationTests.Genres.Commands;

using static Testing;

public class CreateGenreTests : TestBase
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new CreateGenreCommand(null);
        
        // Assert
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldRequireMaximumFields()
    {
        var command = new CreateGenreCommand("TEXTO DE PRUEBA TEXTO DE PRUEBA TEXTO DE PRUEBA TEXTO DE PRUEBA");
        
        // Assert
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }
    
    [Test]
    public async Task ShouldCreateGenre()
    {
        var command = new CreateGenreCommand("Acción");

        var genreId = await SendAsync(command);

        var genre = await FindAsync<Genre>(genreId);
        
        // Assert
        genre.Should().NotBeNull();
        genre.Name.Should().Be("Acción");
        genre.LastModified.Should().BeNull();
    }
}