using Microsoft.EntityFrameworkCore;
using Movies.Domain.Entities;

namespace Movies.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Genre> Genres { get; }
    DbSet<Movie> Movies { get; }
    DbSet<Person> Persons { get; }
    DbSet<Image> Images { get; }
    DbSet<MovieGenre> MovieGenres { get; }
    DbSet<MoviePerson> MoviePersons { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
