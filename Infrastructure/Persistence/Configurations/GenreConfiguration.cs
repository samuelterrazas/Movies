namespace Movies.Infrastructure.Persistence.Configurations;

public class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.Property(g => g.Name)
            .IsRequired()
            .HasMaxLength(50);
            
        builder.Property(g => g.Created)
            .HasColumnType("SMALLDATETIME");
            
        builder.Property(g => g.LastModified)
            .HasColumnType("SMALLDATETIME");
    }
}
