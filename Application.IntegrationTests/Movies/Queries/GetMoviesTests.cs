namespace Movies.Application.IntegrationTests.Movies.Queries;

using static Testing;

public class GetMoviesTests : TestBase
{
    [Test]
    public async Task ShouldReturnAllMovies()
    {
        // Arrange
        await AddRangeAsync(

            new Movie
            {
                Title = "New title",
                Release = 2022,
                Duration = "3h",
                MaturityRating = "18+",
                Summary = "New summary",
                Teaser = "New teaser"
            },
            new Movie
            {
                Title = "New title",
                Release = 2021,
                Duration = "2h 30m",
                MaturityRating = "16+",
                Summary = "New summary",
                Teaser = "New teaser"
            },
            new Movie
            {
                Title = "New title",
                Release = 2020,
                Duration = "2h",
                MaturityRating = "14+",
                Summary = "New summary",
                Teaser = "New teaser"
            }
        );

        // Act
        var query = new GetMoviesQuery(null, null, null, null, null, null);

        var result = await SendAsync(query);

        // Assert
        result.TotalCount.Should().Be(3);
    }
}