using Movies.Application.Genres.Queries.GetGenres;

namespace Movies.Application.IntegrationTests.Genres.Queries;

using static Testing;

public class GetGenresTests : TestBase
{
    [Test]
    public async Task ShouldReturnAllGenres()
    {
        // Arrange
        await AddRangeAsync(
            new Genre {Name = "New name A"}, 
            new Genre {Name = "New name B"}, 
            new Genre {Name = "New name c"}
        );
        
        // Act
        var query = new GetGenresQuery();

        var result = await SendAsync(query);
        
        // Assert
        result.Should().HaveCount(3);
    }
}