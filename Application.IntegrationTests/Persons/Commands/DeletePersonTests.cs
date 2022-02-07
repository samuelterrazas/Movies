using Movies.Application.Persons.Commands.CreatePerson;
using Movies.Application.Persons.Commands.DeletePerson;

namespace Movies.Application.IntegrationTests.Persons.Commands;

using static Testing;

public class DeletePersonTests : TestBase
{
    [Test]
    public async Task ShouldRequireValidPersonId()
    {
        // Act
        var command = new DeletePersonCommand(Id: 99);
        
        // Assert
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeletePerson()
    {
        // Arrange
        var personId = await SendAsync(new CreatePersonCommand(FullName: "New person"));

        // Act
        await SendAsync(new DeletePersonCommand(Id: personId));

        var person = await FindAsync<Person>(personId);

        // Assert\
        person.Should().BeNull();
    }
}