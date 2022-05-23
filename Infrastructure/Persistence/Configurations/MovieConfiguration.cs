namespace Movies.Infrastructure.Persistence.Configurations;

public class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.Property(p => p.Title)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.Release)
            .IsRequired();

        builder.Property(p => p.Duration)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(p => p.MaturityRating)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(p => p.Summary)
            .IsRequired()
            .HasMaxLength(1500);

        builder.Property(p => p.Teaser)
            .IsRequired()
            .HasMaxLength(100);
    }
}
