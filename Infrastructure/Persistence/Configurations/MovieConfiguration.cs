namespace Movies.Infrastructure.Persistence.Configurations;

public class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.Property(m => m.Title)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(m => m.Release)
            .IsRequired()
            .HasColumnType("SMALLINT");

        builder.Property(m => m.Duration)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(m => m.MaturityRating)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(m => m.Summary)
            .IsRequired()
            .HasMaxLength(1500);

        builder.Property(m => m.Created)
            .HasColumnType("SMALLDATETIME");
            
        builder.Property(m => m.LastModified)
            .HasColumnType("SMALLDATETIME");
    }
}
