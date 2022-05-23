namespace Movies.Application.IntegrationTests.Persons.Commands;

using static Testing;

public class UpdatePersonTests : TestBase
{
    [Test]
    public async Task ShouldUpdatePerson()
    {
        // Arrange
        var personId = await SendAsync(new CreatePersonCommand(FullName: "New fullName"));

        // Act
        var command = new UpdatePersonCommand(Id: personId, "Updated fullName");
        await SendAsync(command);

        var genre = await FindAsync<Person>(personId);

        // Assert
        genre.Should().NotBeNull();
        genre!.FullName.Should().Be(command.FullName);
        genre.LastModified.Should().NotBeNull();
    }
}