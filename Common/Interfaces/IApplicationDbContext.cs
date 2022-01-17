using Microsoft.EntityFrameworkCore;
using Movies.Domain.Entities;
using File = Movies.Domain.Entities.File;

namespace Movies.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Genre> Genres { get; }
    DbSet<Movie> Movies { get; }
    DbSet<Person> Persons { get; }
    DbSet<File> Files { get; }
    DbSet<MovieGenre> MovieGenres { get; }
    DbSet<MoviePerson> MoviePersons { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
