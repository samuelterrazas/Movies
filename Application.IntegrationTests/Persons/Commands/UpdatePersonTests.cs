using Movies.Application.Persons.Commands.CreatePerson;
using Movies.Application.Persons.Commands.UpdatePerson;

namespace Movies.Application.IntegrationTests.Persons.Commands;

using static Testing;

public class UpdatePersonTests : TestBase
{
    [Test]
    public async Task ShouldRequireValidPersonId()
    {
        // Act
        var command = new UpdatePersonCommand(Id: 99, FullName: "New person");

        // Assert
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }
    
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
    public async Task ShouldUpdatePerson()
    {
        // Arrange
        var personId = await SendAsync(new CreatePersonCommand(FullName: "New person"));

        // Act
        var command = new UpdatePersonCommand(Id: personId, "Update person full name");
        await SendAsync(command);

        var genre = await FindAsync<Person>(personId);

        // Assert
        genre.Should().NotBeNull();
        genre.FullName.Should().Be(command.FullName);
        genre.LastModified.Should().NotBeNull();
    }
}