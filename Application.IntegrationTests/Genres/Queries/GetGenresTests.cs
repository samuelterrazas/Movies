using Movies.Application.Genres.Queries.GetGenres;

namespace Movies.Application.IntegrationTests.Genres.Queries;

using static Testing;

public class GetGenresTests : TestBase
{
    // Method
    [Test]
    public async Task ShouldReturnAllGenres()
    {
        // Arrange
        await AddRangeAsync(
            new Genre {Name = "New genre 1"}, 
            new Genre {Name = "New genre 2"}, 
            new Genre {Name = "New genre 3"}
        );
        
        // Act
        var query = new GetGenresQuery();

        var result = await SendAsync(query);
        
        // Assert
        result.Should().HaveCount(3);
    }
}