namespace Movies.Infrastructure.Persistence.Configurations;

public class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(50);
    }
}
