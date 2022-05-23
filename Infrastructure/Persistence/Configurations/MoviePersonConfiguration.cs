namespace Movies.Infrastructure.Persistence.Configurations;

public class MoviePersonConfiguration : IEntityTypeConfiguration<MoviePerson>
{
    public void Configure(EntityTypeBuilder<MoviePerson> builder)
    {
        builder.HasKey(p => new {p.MovieId, p.PersonId});

        builder.Property(p => p.MovieId)
            .IsRequired();

        builder.Property(p => p.PersonId)
            .IsRequired();

        builder.Property(p => p.Role)
            .IsRequired();

        builder.Property(p => p.Order)
            .IsRequired();
    }
}
