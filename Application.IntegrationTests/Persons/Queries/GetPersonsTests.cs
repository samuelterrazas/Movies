using Movies.Application.Persons.Queries.GetPersons;

namespace Movies.Application.IntegrationTests.Persons.Queries;

using static Testing;

public class GetPersonsTests : TestBase
{
    [Test]
    public async Task ShouldReturnValidationException()
    {
        // Arrange
        await AddRangeAsync(
            new Person {FullName = "New person 1"},
            new Person {FullName = "New person 2"},
            new Person {FullName = "New person 3"}
        );
        
        // Act
        var query = new GetPersonsQuery(0, 0, "");

        // Assert
        await FluentActions.Invoking(() => SendAsync(query)).Should().ThrowAsync<ValidationException>();
    }
    
    [Test]
    public async Task ShouldReturnAllPersons()
    {
        // Arrange
        await AddRangeAsync(
            new Person {FullName = "New person 1"},
            new Person {FullName = "New person 2"},
            new Person {FullName = "New person 3"}
        );

        // Act
        var query = new GetPersonsQuery(null, null, null);

        var result = await SendAsync(query);

        // Assert
        result.TotalCount.Should().Be(3);
    }
}