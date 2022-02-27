using Movies.Application.Persons.Commands.CreatePerson;
using Movies.Application.Persons.Queries.GetPersonDetails;

namespace Movies.Application.IntegrationTests.Persons.Queries;

using static Testing;

public class GetPersonDetailsTests : TestBase
{
    [Test]
    public async Task ShouldReturnPersonDetails()
    {
        // Arrange
        var personId = await SendAsync(new CreatePersonCommand(FullName: "New fullName"));
        
        // Act
        var query = new GetPersonDetailsQuery(Id: personId);

        var result = await SendAsync(query);

        // Assert
        result.Id.Should().Be(personId);
    }
}