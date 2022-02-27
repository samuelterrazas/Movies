using Movies.Application.Persons.Queries.GetPersons;

namespace Movies.Application.IntegrationTests.Persons.Queries;

using static Testing;

public class GetPersonsTests : TestBase
{
    [Test]
    public async Task ShouldReturnAllPersons()
    {
        // Arrange
        await AddRangeAsync(
            new Person {FullName = "New fullName A"},
            new Person {FullName = "New fullName B"},
            new Person {FullName = "New fullName C"}
        );

        // Act
        var query = new GetPersonsQuery(null, null, null);

        var result = await SendAsync(query);

        // Assert
        result.TotalCount.Should().Be(3);
    }
}