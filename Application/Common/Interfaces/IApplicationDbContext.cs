using Microsoft.EntityFrameworkCore;
using Movies.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Movies.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Genre> Genres { get; set; }
        DbSet<Movie> Movies { get; set; }
        DbSet<Person> Persons { get; set; }
        DbSet<MovieGenre> MovieGenres { get; set; }
        DbSet<MoviePerson> MoviePersons { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
