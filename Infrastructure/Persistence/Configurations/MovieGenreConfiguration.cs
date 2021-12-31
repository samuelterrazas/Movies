using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movies.Domain.Entities;

namespace Movies.Infrastructure.Persistence.Configurations;

public class MovieGenreConfiguration : IEntityTypeConfiguration<MovieGenre>
{
    public void Configure(EntityTypeBuilder<MovieGenre> builder)
    {
        builder.HasKey(m => new {m.GenreId, m.MovieId});

        builder.Property(m => m.GenreId)
            .IsRequired();

        builder.Property(m => m.MovieId)
            .IsRequired();
    }
}
