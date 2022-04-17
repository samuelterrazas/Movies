namespace Movies.Infrastructure.Persistence.Configurations;

public class MoviePersonConfiguration : IEntityTypeConfiguration<MoviePerson>
{
    public void Configure(EntityTypeBuilder<MoviePerson> builder)
    {
        builder.HasKey(m => new {m.MovieId, m.PersonId});

        builder.Property(m => m.MovieId)
            .IsRequired();

        builder.Property(m => m.PersonId)
            .IsRequired();

        builder.Property(m => m.Role)
            .IsRequired()
            .HasColumnType("TINYINT");

        builder.Property(m => m.Order)
            .IsRequired()
            .HasColumnType("TINYINT");
    }
}
