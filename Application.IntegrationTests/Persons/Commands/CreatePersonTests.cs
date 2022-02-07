using Movies.Application.Persons.Commands.CreatePerson;

namespace Movies.Application.IntegrationTests.Persons.Commands;

using static Testing;

public class CreatePersonTests : TestBase
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        // Act
        var command = new CreatePersonCommand(FullName: "");
        
        // Assert
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldRequireMaximumFields()
    {
        // Act
        var command = new CreatePersonCommand(FullName: "TEXT TEST TEXT TEST TEXT TEST TEXT TEST TEXT TEST TEXT TEST " +
                                                        "TEXT TEST TEXT TEST TEXT TEST TEXT TEST TEXT TEST TEXT TEST");
        
        // Assert
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldCreatePerson()
    {
        // Act
        var command = new CreatePersonCommand(FullName: "New person");

        var personId = await SendAsync(command);

        var genre = await FindAsync<Person>(personId);

        // Assert
        genre.Should().NotBeNull();
        genre.FullName.Should().Be("New person");
        genre.LastModified.Should().BeNull();
    }
}