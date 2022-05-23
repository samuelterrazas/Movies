namespace Movies.Infrastructure.Persistence.Configurations;

public class MovieGenreConfiguration : IEntityTypeConfiguration<MovieGenre>
{
    public void Configure(EntityTypeBuilder<MovieGenre> builder)
    {
        builder.HasKey(p => new {p.GenreId, p.MovieId});

        builder.Property(p => p.GenreId)
            .IsRequired();

        builder.Property(p => p.MovieId)
            .IsRequired();
    }
}
