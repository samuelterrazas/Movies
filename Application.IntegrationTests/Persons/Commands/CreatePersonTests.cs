namespace Movies.Application.IntegrationTests.Persons.Commands;

using static Testing;

public class CreatePersonTests : TestBase
{
    [Test]
    public async Task ShouldCreatePerson()
    {
        // Act
        var command = new CreatePersonCommand(FullName: "New fullName");

        var personId = await SendAsync(command);

        var genre = await FindAsync<Person>(personId);

        // Assert
        genre.Should().NotBeNull();
        genre!.FullName.Should().Be("New fullName");
        genre.LastModified.Should().BeNull();
    }
}