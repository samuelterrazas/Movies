namespace Movies.Infrastructure.Persistence.Configurations;

public class ImageConfiguration : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        builder.Property(p => p.MovieId)
            .IsRequired();

        builder.Property(p => p.Url)
            .IsRequired()
            .HasMaxLength(200);
    }
}